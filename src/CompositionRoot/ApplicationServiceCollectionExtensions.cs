using Apps.Common;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;

namespace Apps.CompositionRoot
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddSingleton<ISystemClock, SystemClock>();

            /*services.AddScoped<IApplication, Application>();*/

            return services;
        }
    }
}