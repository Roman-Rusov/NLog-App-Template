using System.Threading;
using System.Threading.Tasks;

using Apps.Common.Naming;

namespace Apps.Application.Contracts
{
    public interface IApplicationTask
    {
        public string Name => GetType().Name.RemoveFirstSuffix("ApplicationTask", "AppTask");

        Task Run(CancellationToken ct = default);
    }
}