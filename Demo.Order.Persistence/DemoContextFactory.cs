using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Demo.Order.Persistence
{
    public class DemoContextFactory : IDesignTimeDbContextFactory<DemoContext>
    {
        public DemoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
            optionsBuilder
                .UseNpgsql("Host=localhost;port=5432;Database=demo_event_driven;Username=postgres;Password=postgres")
                .UseSnakeCaseNamingConvention();

            return new DemoContext(optionsBuilder.Options);
        }
    }
}