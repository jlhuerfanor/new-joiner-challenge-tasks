
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

            services.AddSingleton<MapperConfiguration>(MapperConfigurationExtension.CreateConfiguration);
            services.AddSingleton<Mapper>(MapperConfigurationExtension.CreateMapper);

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
            });

            return mapperConfiguration;
        }
    }
}
