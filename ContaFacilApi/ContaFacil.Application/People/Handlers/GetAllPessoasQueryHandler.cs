using MediatR;
using ContaFacil.Application.People.Queries;
using ContaFacil.Application.People.ViewModels;
using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Application.People.Handlers
{
    public class GetAllPessoasQueryHandler
        : IRequestHandler<GetAllPessoasQuery, List<PessoaViewModel>>
    {
        private readonly IPessoaRepository _repository;

        public GetAllPessoasQueryHandler(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PessoaViewModel>> Handle(
            GetAllPessoasQuery request,
            CancellationToken cancellationToken)
        {
            var pessoas = await _repository.GetAllAsync();

            return pessoas.Select(p => new PessoaViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade
            }).ToList();
        }
    }
}
