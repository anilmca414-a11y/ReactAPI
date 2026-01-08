using DemoDotNetCore.Models;

namespace DemoDotNetCore.Services
{
    public interface ITenantProvider
    {
        Tenant GetTenant();
        void SetTenant(Tenant tenant);
    }

}
