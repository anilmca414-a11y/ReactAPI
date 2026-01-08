using DemoDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDomainController : ControllerBase
    {
        private readonly ITenantResolver _tenantResolver;
        public GetDomainController(ITenantResolver tenantResolver)
        {
            _tenantResolver = tenantResolver;
        }
        [HttpGet("tenant")]
        public IActionResult GetTenant()
        {
            var tenantInfo = _tenantResolver.Resolve(HttpContext);
            return Ok(tenantInfo);
        }
       
        //https://tenant2.localhost:44364/api/GetDomain/tenant
            //Response
            //{
            //  "name": "tenant2",
            //  "host": "tenant2.localhost"
            //}
}
}
