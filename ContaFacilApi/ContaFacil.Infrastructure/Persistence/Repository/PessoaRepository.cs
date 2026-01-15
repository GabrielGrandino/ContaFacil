using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ContaFacilDbContext _context;

        public PessoaRepository(ContaFacilDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }
    }
}
