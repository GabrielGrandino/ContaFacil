using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaFacil.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Descricao)
                .HasColumnName("descricao")
                .IsRequired();

            builder.Property(c => c.PurposeId)
                .HasColumnName("finalidade_id");

            builder.HasOne(c => c.Purpose)
                .WithMany(p => p.Categories)
                .HasForeignKey(c => c.PurposeId);
        }
    }
}
