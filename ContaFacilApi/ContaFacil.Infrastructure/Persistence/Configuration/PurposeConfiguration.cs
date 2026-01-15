using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaFacil.Infrastructure.Persistence.Configurations
{
    public class PurposeConfiguration : IEntityTypeConfiguration<Purpose>
    {
        public void Configure(EntityTypeBuilder<Purpose> builder)
        {
            builder.ToTable("purpose");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.Descricao)
                .HasColumnName("finalidade");
        }
    }
}
