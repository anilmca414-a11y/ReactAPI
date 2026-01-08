using DemoDotNetCore.Models;
using DemoDotNetCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DemoDotNetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        //private readonly ITenantProvider _tenantProvider;
        //private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //_tenantProvider = tenantProvider;
            //_configuration = configuration;
        }
       
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var connectionString =
        //        _configuration.GetConnectionString(_tenantProvider.Tenant);

        //        if (string.IsNullOrEmpty(connectionString))
        //            throw new Exception($"Connection string not found for tenant {_tenantProvider.Tenant}");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //    //var tenant = _tenantProvider.GetTenant();
        //    //optionsBuilder.UseSqlServer(tenant.ConnectionString);
        //}
        //public class ApplicationDbContext : DbContext
        //{
        //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //        : base(options)
        //    {
        //    }

        //    public DbSet<User> Users { get; set; }
        //    public DbSet<Student> Students { get; set; }
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}