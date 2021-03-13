
using Tasks.Business.Status;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBusinessConfigurationExtension
    {
        public static IServiceCollection ConfigureApplicationBusiness(this IServiceCollection services) {
            services.AddSingleton<GetStatusBusiness>();
            return services;
        }
    }
}