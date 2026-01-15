using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence
{
    public class ContaFacilDbContext : DbContext
    {
        public ContaFacilDbContext(DbContextOptions<ContaFacilDbContext> options)
            : base(options) { }

        public DbSet<Person> Pessoas => Set<Person>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ContaFacilDbContext).Assembly);
        }
    }
}
