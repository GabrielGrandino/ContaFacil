using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ContaFacilDbContext _context;

        public PersonRepository(ContaFacilDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Person pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Pessoas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task DeleteAsync(Person pessoa)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}
