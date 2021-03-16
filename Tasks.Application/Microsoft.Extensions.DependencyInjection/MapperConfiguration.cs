
using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tasks.Model.Client;
using Tasks.Model.Domain;
using Tasks.Service.Conversion;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MapperConfigurationExtension
    {
        public static IServiceCollection ConfigureMapper(this IServiceCollection services) {

            services.AddScoped<MapperConfiguration>(MapperConfigurationExtension.CreateConfiguration);
            services.AddScoped<Mapper>(MapperConfigurationExtension.CreateMapper);
            services.AddScoped<TaskDtoToTaskConverter>();

            return services;
        }

        private static Mapper CreateMapper(IServiceProvider provider)
        {
            return new Mapper(provider.GetService<MapperConfiguration>());
        }

        private static MapperConfiguration CreateConfiguration(IServiceProvider serviceProvider)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<ServiceStatus, ServiceStatusDto>().ConvertUsing<ServiceStatusToServiceStatusDtoConverter>();
                cfg.CreateMap<TaskDto, Task>().ConvertUsing(serviceProvider.GetService<TaskDtoToTaskConverter>());
                cfg.CreateMap<Task, TaskDto>().ConvertUsing(serviceProvider.GetService<TaskDtoToTaskConverter>());
            });

            return mapperConfiguration;
        }
    }
}
