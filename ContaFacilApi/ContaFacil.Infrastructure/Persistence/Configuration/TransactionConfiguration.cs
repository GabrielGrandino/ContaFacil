using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaFacil.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transaction");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.Descricao)
                .HasColumnName("descricao")
                .IsRequired();

            builder.Property(t => t.Valor)
                .HasColumnName("valor")
                .HasPrecision(18, 2);

            builder.Property(t => t.TipoId)
                .HasColumnName("tipo_id");

            builder.Property(t => t.CategoryId)
                .HasColumnName("categoria_id");

            builder.Property(t => t.PessoaId)
                .HasColumnName("pessoa_id");

            builder.HasOne(t => t.Finalidade)
                .WithMany()
                .HasForeignKey(t => t.TipoId);

            builder.HasOne(t => t.Categoria)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);

            builder.HasOne(t => t.Pessoa)
                .WithMany()
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
