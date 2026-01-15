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
        public DbSet<Category> Categorias => Set<Category>();
        public DbSet<Purpose> Finalidade => Set<Purpose>();

        //Passa para o configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ContaFacilDbContext).Assembly);
        }
    }
}
