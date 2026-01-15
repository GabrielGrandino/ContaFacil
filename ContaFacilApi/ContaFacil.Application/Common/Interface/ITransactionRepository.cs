namespace ContaFacil.Application.Common.Interfaces
{
    public interface ITransactionRepository
    {
        Task DeleteByPessoaIdAsync(Guid pessoaId);
    }
}
