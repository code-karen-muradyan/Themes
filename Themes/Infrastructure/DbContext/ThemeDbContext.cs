using Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Themes.API.Infrastructure.DbContext
{
    public class ThemeDbContext : BaseDBContext<ThemeDbContext>
    {
        public ThemeDbContext()
        {

        }
        public ThemeDbContext(string prefix, string connectionString) : base(prefix, connectionString)
        {

        }
        public ThemeDbContext(DbContextOptions<ThemeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Themes");
        }
    }
}