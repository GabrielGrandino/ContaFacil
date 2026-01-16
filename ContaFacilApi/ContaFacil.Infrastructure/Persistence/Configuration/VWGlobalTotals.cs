using ContaFacil.Application.Reports.DTOs;
using ContaFacil.Application.Reports.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Infrastructure.Persistence.Configuration
{
    public class VWGlobalTotalsConfiguration : IEntityTypeConfiguration<GlobalTotalsViewModel>
    {
        public void Configure(EntityTypeBuilder<GlobalTotalsViewModel> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_person_totals");

            builder.Property(e => e.TotalReceitas).HasColumnName("total_receitas");
            builder.Property(e => e.TotalDespesas).HasColumnName("total_despesas");
            builder.Property(e => e.SaldoGeral).HasColumnName("saldo_geral");
        }
    }
}
