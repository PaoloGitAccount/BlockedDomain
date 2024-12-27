using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Autofac;
using WebApi.Configurations;
using Hangfire;
using Microsoft.OpenApi.Models;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<BlockedDomainContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<BlockedDomainContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

        services.AddMemoryCache();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("Redis");
        });

        services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();

        services.AddResponseCompression();

        services.AddHealthChecks()
            .AddCheck("API Health", () => HealthCheckResult.Healthy("The API is healthy"));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new AutofacModule());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseRouting();
        app.UseResponseCompression();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHealthChecks("/health");

        app.UseHangfireDashboard();
        app.UseHangfireServer();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
