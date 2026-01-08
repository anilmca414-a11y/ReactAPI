using DemoDotNetCore.Services;
using DemoDotNetCore.Models;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DemoDotNetCore.Middleware
{
    public class TenantMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, ITenantProvider tenantProvider)
        {
            var host = context.Request.Host.Host;
            //var clientId = context.User.FindFirst("Client-Id")?.Value;

            var tenantConfig = _configuration
                .GetSection("Tenants")
                .Get<Tenant[]>();

            var tenant = tenantConfig?
                .FirstOrDefault(t => host.Contains(t.name));

            if (tenant == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Tenant not found");
                return;
            }

            tenantProvider.SetTenant(tenant);

            await _next(context);
        }

    }
}
