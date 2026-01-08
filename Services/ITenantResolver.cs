using DemoDotNetCore.Models;

namespace DemoDotNetCore.Services
{
    public interface ITenantResolver
    {
        TenantInfo Resolve(HttpContext context);
    }
}
