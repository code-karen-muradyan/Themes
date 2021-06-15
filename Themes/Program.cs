using Themes.API.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Themes.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var _appName = "";
            try
            {
                (var hostBuilder, var appName) = typeof(Program).Assembly.CreateHostBuilder<Startup>(args);
                var host = hostBuilder.Build();
                _appName = appName;
                host.MigrateDbContext<ThemeDbContext>((context, services) => { });
                Log.Information("Applying migrations ({ApplicationContext})...", appName);
                Log.Information("Starting web host ({ApplicationContext})...", appName);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", _appName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}