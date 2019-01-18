using AssetTracker.Api.Authorization;
using AssetTracker.Core;
using AssetTracker.Core.Services;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AssetTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        public IConfiguration _config { get; }
        private IHostingEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    "MustOwnAsset",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.AddRequirements(
                                new MustOwnAssetRequirement());
                    });

                authorizationOptions.AddPolicy(
                    "MustBelongToOrganization",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.AddRequirements(
                                new MustBelongToOrganizationRequirement());
                    });
            });

            services.AddScoped<IAuthorizationHandler, MustOwnAssetHandler>();

            services.AddAuthentication(
                IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44374/";
                    options.ApiName = "assettrackerapi";
                    options.ApiSecret = "apisecret";
                });

            services.AddSingleton(_config);
            services.AddAutoMapper();
            services.AddDbContext<AssetTrackerContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("CgaData")),
                ServiceLifetime.Scoped);
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetLocationService, AssetLocationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
