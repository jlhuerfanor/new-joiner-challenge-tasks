using Tasks.Model.Domain;
using Tasks.Service.Status;

namespace Tasks.Business.Status
{
    public class GetStatusBusiness {
        private IStatusService statusService_;

        public GetStatusBusiness(IStatusService statusService) {
            this.statusService_ = statusService;
        }

        public ServiceStatus GetStatus() {
            return this.statusService_.GetStatus();
        }
    }
}