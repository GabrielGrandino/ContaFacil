using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ContaFacilDbContext _context;

        //Injeta context
        public TransactionRepository(ContaFacilDbContext context)
        {
            _context = context;
        }

        //Adiciona ao banco de dados
        public async Task AddAsync(Transaction transaction)
        {
            _context.Transacoes.Add(transaction);
            await _context.SaveChangesAsync();
        }

        //Pega todas as transações
        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transacoes
                .Include(t => t.Categoria)
                    .ThenInclude(c => c.Purpose)
                .Include(t => t.Pessoa)
                .ToListAsync();
        }

        //pega as informações das finalidades
        public async Task<Purpose?> GetPurposeAsync(int finalidadeId)
        {
            return await _context.Finalidade.FindAsync(finalidadeId);
        }

        //pegas as informações da pessoa
        public async Task<Person?> GetPessoaAsync(Guid pessoaId)
        {
            return await _context.Pessoas.FindAsync(pessoaId);
        }

        //pega as informações da categoria
        public async Task<Category?> GetCategoryAsync(int categoryId)
        {
            return await _context.Categorias
                .Include(c => c.Purpose)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
