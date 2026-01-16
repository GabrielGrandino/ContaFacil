using ContaFacil.Application.Common.Interface;
using ContaFacil.Application.Reports.DTOs;
using ContaFacil.Application.Reports.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Infrastructure.Persistence.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly ContaFacilDbContext _context;

        public ReportsRepository(ContaFacilDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonTotalsViewModel>> ObterTotaisPorPessoaAsync()
        {
            return await _context.PessoaTotais
                .AsNoTracking()
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<GlobalTotalsViewModel?> ObterTotaisGeralAsync()
        {
            return await _context.TotaisGeral
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }

}
