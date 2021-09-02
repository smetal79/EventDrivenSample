using Demo.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Order.Persistence
{
    public sealed class DemoContext : DbContext
    {
        public DbSet<Domain.Entities.Order> Orders { get; set; }

        public DemoContext(DbContextOptions<DemoContext> options)
        : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.Order>(e =>
            {
                e.HasKey(p => p.Key);
            });

            modelBuilder.Entity<ProcessedOrder>();
            modelBuilder.Entity<DraftOrder>();
            modelBuilder.Entity<SubmittedOrder>();
        }
    }
}