using Tasks.Model.Domain;

namespace Tasks.Service.Status
{
    public class DefaultStatusService : IStatusService
    {
        public ServiceStatus GetStatus()
        {
            return new ServiceStatus() {
                Status = "OK"
            };
        }
    }
}