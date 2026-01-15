using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Infrastructure.Persistence.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task DeleteByPessoaIdAsync(Guid pessoaId)
        {
            // Ainda não implementado
            return Task.CompletedTask;
        }
    }
}
