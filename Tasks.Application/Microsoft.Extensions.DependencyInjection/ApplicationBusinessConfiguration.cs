
using Tasks.Business.Status;
using Tasks.Business.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBusinessConfigurationExtension
    {
        public static IServiceCollection ConfigureApplicationBusiness(this IServiceCollection services) {
            services.AddSingleton<GetStatusBusiness>();
            services.AddScoped<CreateTaskBusiness>();
            return services;
        }
    }
}