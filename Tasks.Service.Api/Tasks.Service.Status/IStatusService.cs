using Tasks.Model.Domain;

namespace Tasks.Service.Status {

    public interface IStatusService
    {
        ServiceStatus GetStatus();
    }
    
}