using ContaFacil.Application.Reports.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Infrastructure.Persistence.Configuration
{
    public class VWPersonTotalsConfiguration : IEntityTypeConfiguration<PersonTotalsViewModel>
    {
        public void Configure(EntityTypeBuilder<PersonTotalsViewModel> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_person_totals");

            builder.Property(e => e.PessoaId).HasColumnName("pessoa_id");
            builder.Property(e => e.Nome).HasColumnName("nome");
            builder.Property(e => e.TotalReceitas).HasColumnName("total_receitas");
            builder.Property(e => e.TotalDespesas).HasColumnName("total_despesas");
            builder.Property(e => e.Saldo).HasColumnName("saldo");
        }
    }
}
