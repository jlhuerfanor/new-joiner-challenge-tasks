
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tasks.Business.Status;
using Tasks.Model.Client;

namespace Tasks.Web.Controller
{
    [ApiController]
    [Route("status")]
    public class StatusController
    {
        private GetStatusBusiness statusBusiness;
        private Mapper mapper;

        public StatusController(
            GetStatusBusiness statusBusiness,
            AutoMapper.Mapper mapper) {
            this.statusBusiness = statusBusiness;
            this.mapper = mapper;
        }

        [HttpGet]
        public ServiceStatusDto GetStatus() {
            return mapper.Map<ServiceStatusDto>(this.statusBusiness.GetStatus());
        }
    }
    
}