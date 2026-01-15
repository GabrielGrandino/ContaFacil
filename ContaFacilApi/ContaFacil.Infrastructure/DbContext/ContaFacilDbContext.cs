using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence
{
    public class ContaFacilDbContext : DbContext
    {
        public ContaFacilDbContext(DbContextOptions<ContaFacilDbContext> options)
            : base(options) { }

        //Tabelas
        public DbSet<Person> Pessoas => Set<Person>();

        //Passa para o configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ContaFacilDbContext).Assembly);
        }
    }
}
