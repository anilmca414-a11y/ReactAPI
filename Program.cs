using System.Text;
using DemoDotNetCore.Data;
using DemoDotNetCore.Middleware;
using DemoDotNetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// =======================
// CORS
// =======================
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// =======================
// JWT Authentication
// =======================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// =======================
// Services
// =======================
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

// =======================
// DbContext (Tenant-aware)
// =======================
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
    var tenant = tenantProvider.GetTenant();

    if (tenant == null || string.IsNullOrEmpty(tenant.ConnectionStrings))
        throw new Exception("Tenant connection string not resolved");

    options.UseSqlServer(tenant.ConnectionStrings);
});

// =======================
// Identity
// =======================
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddScoped<ITenantResolver, TenantResolver>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =======================
// Middleware ORDER MATTERS
// =======================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔴 TENANT MUST COME FIRST
app.UseMiddleware<TenantMiddleware>();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
