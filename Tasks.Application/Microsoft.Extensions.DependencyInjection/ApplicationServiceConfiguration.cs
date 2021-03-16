
using Tasks.Service.Status;
using Tasks.Service.Persistence;
using Tasks.Service.Tasks;
using Tasks.Service.Joiner;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceConfigurationExtension
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddSingleton<IStatusService, DefaultStatusService>();
            services.AddSingleton<JsonSerializerOptions>(CreateJsonSerializerOptions);
            services.AddScoped<ITransactionalService, EFTransactionalService>();
            services.AddScoped<ITaskRepositoryService, EFTaskRepositoryService>();
            services.AddScoped<ITaskQueryService, EFTaskQueryService>();
            services.AddScoped<IJoinerQueryService, RestJoinerQueryService>(CreateRestJoinerQueryService(configuration));
            
            return services;
        }

        private static JsonSerializerOptions CreateJsonSerializerOptions(IServiceProvider arg)
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.Web) {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public static Func<IServiceProvider, RestJoinerQueryService> CreateRestJoinerQueryService(IConfiguration configuration) {
            return (provider) => new RestJoinerQueryService(
                provider.GetService<ILogger<RestJoinerQueryService>>(),
                provider.GetService<JsonSerializerOptions>(),
                configuration["JoinerService:Url"],
                configuration.GetValue<int>("JoinerService:RequestTimeout"));
        }
    }
}