using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaFacil.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("person");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("id");

            entity.Property(p => p.Nome)
                  .HasColumnName("nome")
                  .IsRequired();

            entity.Property(p => p.Idade)
                  .HasColumnName("idade")
                  .IsRequired();

            entity.Property(p => p.CreatedAt)
                  .HasColumnName("created_at")
                  .HasDefaultValueSql("now()");

            entity.Property(p => p.Ativo)
                  .HasColumnName("ativo")
                  .HasDefaultValue(1);
        }
    }
}
