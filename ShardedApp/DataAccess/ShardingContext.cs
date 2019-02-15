using Microsoft.EntityFrameworkCore;

namespace ShardedApp.DataAccess
{
    public class ShardingContext : DbContext
    {
        private AppTenant tenant;

        public ShardingContext(AppTenant tenant)
        {
            this.tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(tenant.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(k => k.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
