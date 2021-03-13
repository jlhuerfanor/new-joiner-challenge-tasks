using AutoMapper;
using Tasks.Model.Client;
using Tasks.Model.Domain;

namespace Tasks.Service.Conversion
{
    public class ServiceStatusToServiceStatusDtoConverter
        : ITypeConverter<ServiceStatus, ServiceStatusDto>
    {
        public ServiceStatusDto Convert(ServiceStatus source, ServiceStatusDto destination, ResolutionContext context)
        {
            destination = (destination != null) ? destination : new ServiceStatusDto();
            destination.Status = source.Status;

            return destination;
        }
    }

}