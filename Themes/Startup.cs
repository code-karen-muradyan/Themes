using Autofac;
using Common;
using Services.Infrastructure.Modules;
using Themes.API.Controllers;
using Themes.API.Extensions;
using Themes.API.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using MongoStore.Services;
using Newtonsoft.Json;
using System;
using System.Reflection;
using UserPrivilegePolicy.Services;

namespace Themes.API
{
    public class Startup : CommonStartupBase
    {
        const string PREFIX = "Themes";
        public override ServiceInfo ServiceInfo { get; }
        public Startup(IWebHostEnvironment env) : base(env)
        {
            ServiceInfo = new ServiceInfo(
                "themesapi",
                "Themes API",
                PREFIX,
                serviceDescription: "Manages views",
                usesAuthentication: true,
                audience: "theme",
                trustedAudience: "themes",
                usesSwagger: true,
                swaggerUIClientId: "themeswaggerui");

            ServiceInfo.RequiredScopes.Add(("theme", "Themes API"));
        }
        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddRepositories(PREFIX);
            services.AddTransient<IMongoStoreService, MongoStoreService>(sp =>
            {
                var dbConnections = sp.GetRequiredService<DbConnections>();
                return new MongoStoreService(dbConnections.MongoConnectionString);
            });
            return base.ConfigureServices(services);
        }
        protected override void RegisterModules(ContainerBuilder container)
        {
            container.RegisterModule(new MediatorModule(typeof(Startup).GetTypeInfo().Assembly));
        }
        protected override void ConfigureJsonOptions(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
        protected override void ConfigureApiVersioning(ApiVersioningOptions o)
        {
            o.Conventions.Controller<ThemeController>().IsApiVersionNeutral();
            o.Conventions.Controller<UserThemeController>().IsApiVersionNeutral();
            o.Conventions.Controller<STOSThemeController>().IsApiVersionNeutral();
        }
        protected override void ConfigureDatabaseServices(IServiceCollection services)
        {
            ConfigureDbContext<ThemeDbContext>(services);
        }
    }
}