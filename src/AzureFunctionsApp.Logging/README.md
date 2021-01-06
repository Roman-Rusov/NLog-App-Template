# Azure Functions Logging

## Log level
First, log level filtering defined in [host.json](../AzureFunctionsApp/host.json) is applied. Then, if the app is run locally, rules in [local.settings.json](../AzureFunctionsApp/local.settings.json) _override_ [host.json](../AzureFunctionsApp/host.json)'s log levels.

After that, NLog filtering defined in NLog confg files ([NLog.Development.config](NLog.Development.config) and [NLog.Production.config](NLog.Production.config), depending on environment) as applied _in addition_ to previously defined log level filters. I.e. NLog config _doesn't override_, but only either further restricts the filters or leaves them as. It cannot weaken the previously applied log level filtering.

## NLog internal logs
In Production, i.e. in Azure, when Function is deployed there, NLog writes its own internal log to the `C:\home\LogFiles\Application\Functions\NLog\nlog-internal.log` file (defined in the `nlog[internalLogFile]` attribute in the [NLog.Production.config](NLog.Production.config) file). Please check the log file if encountering any issues with logging configuration. The file content can be listed, for instance, in Console section on the Function resource page in Azure Portal. Just execute the following command:
```Shell
cat C:\home\LogFiles\Application\Functions\NLog\nlog-internal.log
```
To flush the file after resolving an issue, just execute:
```Shell
break>C:\home\LogFiles\Application\Functions\NLog\nlog-internal.log
```

## excludeProperties issue
The value set to the `parameter[name="@EventProperties"]/layout[excludeProperties]` attribute in the [NLog.Production.config](NLog.Production.config) file doesn't take effect. Maybe this is a bug. To avoid exceeding telemetry data, `maxRecursionLimit` is set to `1`.

## Seq target
In order to use `Seq` defined in [NLog.Development.config](NLog.Development.config), download and install it from the [Seq official site](https://datalust.co/download) or use docker to run [Seq container](https://hub.docker.com/r/datalust/seq), e.g.:
```Shell
docker run -d --restart unless-stopped --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```

## Database target
Run the [Create_Log_table.sql](scripts/init-logs-table.sql) script in order to setup `Logs` database. Provide connection string to the database in the `<connectionString>` node in NLog settings ([Development](NLog.Development.config#L44), [Production](NLog.Production.config#L12)).