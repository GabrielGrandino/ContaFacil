using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Common.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<List<Transaction>> GetAllAsync();

        Task<Purpose> GetPurposeAsync(int finalidadeId);
        Task<Person?> GetPessoaAsync(Guid pessoaId);
        Task<Category?> GetCategoryAsync(int categoryId);
    }
}
