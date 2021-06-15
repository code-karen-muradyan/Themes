using Services.Infrastructure;
using Services.Infrastructure.Idempotency;
using Themes.API.Infrastructure.DbContext;
using Themes.API.Infrastructure.Repositories;
using Themes.API.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Themes.API.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services, string servicePrefix)
        {
            services.AddScoped<IUnitOfWork>(sp =>
            {
                var dbConnections = sp.GetRequiredService<IOptions<DbConnections>>().Value;
                return new ThemeDbContext(servicePrefix, dbConnections.MainConnection);
            });
            services.AddScoped<IUnitOfWork>(sp =>
            {
                var dbConnections = sp.GetRequiredService<IOptions<DbConnections>>().Value;
                return new BaseMongoStore<UserTheme>(dbConnections.MongoConnectionString, servicePrefix, "Themes");
            });
            services.AddScoped<IIdempotencyRepository>(sp =>
            {
                var unitOfWork = sp.GetUnitOfWork(UnitOfWorkType.Sql);
                var context = unitOfWork.GetFactory() as ThemeDbContext;
                return context;
            });
            services.AddTransient<IThemesRepository, ThemesRepository>(sp =>
            {
                var unitOfWork = sp.GetUnitOfWork(UnitOfWorkType.MongoDB);
                var context = unitOfWork.GetFactory() as BaseMongoStore<UserTheme>;
                return new ThemesRepository(context);
            });
        }
    }
}