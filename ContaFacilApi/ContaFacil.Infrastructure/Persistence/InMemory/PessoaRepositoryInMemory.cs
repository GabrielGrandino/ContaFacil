using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;

namespace ContaFacil.Infrastructure.Persistence.InMemory
{
    public class PessoaRepositoryInMemory : IPessoaRepository
    {
        private static readonly List<Pessoa> _pessoas = new();

        public void Add(Pessoa pessoa)
        {
            _pessoas.Add(pessoa);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoas;
        }
    }
}
