
using Tasks.Service.Status;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceConfigurationExtension
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services) {
            services.AddSingleton<IStatusService, DefaultStatusService>();
            return services;
        }
    }
}