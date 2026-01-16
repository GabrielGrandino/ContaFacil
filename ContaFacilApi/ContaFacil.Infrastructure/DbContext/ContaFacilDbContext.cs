using ContaFacil.Application.Reports.DTOs;
using ContaFacil.Application.Reports.ViewModel;
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
        public DbSet<Transaction> Transacoes => Set<Transaction>();

        //Views
        public DbSet<PersonTotalsViewModel> PessoaTotais => Set<PersonTotalsViewModel>();
        public DbSet<GlobalTotalsViewModel> TotaisGeral => Set<GlobalTotalsViewModel>();

        //Passa para o configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ContaFacilDbContext).Assembly);
        }
    }
}
