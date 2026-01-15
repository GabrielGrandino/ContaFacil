using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Common.Interfaces
{
    public interface IPessoaRepository
    {
        Task AddAsync(Pessoa pessoa);
        Task<IEnumerable<Pessoa>> GetAllAsync();
    }
}
