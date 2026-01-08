using DemoDotNetCore.Data;
using DemoDotNetCore.Models;

namespace DemoDotNetCore.Services
{
    public class TenantProvider : ITenantProvider
    {
        private Tenant _tenant;

        public Tenant GetTenant() => _tenant;

        public void SetTenant(Tenant tenant)
        {
            _tenant = tenant;
        }


    }
}
