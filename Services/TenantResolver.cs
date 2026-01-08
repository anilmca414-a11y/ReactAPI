using DemoDotNetCore.Models;

namespace DemoDotNetCore.Services
{
    public class TenantResolver:ITenantResolver
    {
        public TenantInfo Resolve(HttpContext context)
        {
            var host = context.Request.Host.Host; // tenant2.localhost

            string tenant;

            if (host.Contains("."))
                tenant = host.Split('.')[0];   // tenant2
            else
                tenant = host;                 // tenant2 (no domain case)

            return new TenantInfo
            {
                Name = tenant,
                Host = host
            };
        }
    }
}
