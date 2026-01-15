using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Common.Interfaces
{
    public interface IPessoaRepository
    {
        void Add(Pessoa pessoa);
        IEnumerable<Pessoa> GetAll();
    }
}
