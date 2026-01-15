using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ContaFacilDbContext _context;

        //Injeta o context
        public PersonRepository(ContaFacilDbContext context)
        {
            _context = context;
        }

        //Função para adicionar o objeto pessoa informado ao banco
        public async Task AddAsync(Person pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        //Função que traz uma lista de pessoas cadastradas
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        //Função que traz a pessoa informada pelo id
        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Pessoas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Função para deletar o objeto pessoa informado
        public async Task DeleteAsync(Person pessoa)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}
