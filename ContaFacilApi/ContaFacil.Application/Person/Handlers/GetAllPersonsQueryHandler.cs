using MediatR;
using ContaFacil.Application.People.Queries;
using ContaFacil.Application.People.ViewModels;
using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Application.People.Handlers
{
    public class GetAllPersonsQueryHandler
        : IRequestHandler<GetAllPersonsQuery, List<PersonViewModel>>
    {
        private readonly IPersonRepository _repository;

        public GetAllPersonsQueryHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PersonViewModel>> Handle(
            GetAllPersonsQuery request,
            CancellationToken cancellationToken)
        {
            var pessoas = await _repository.GetAllAsync();

            return pessoas.Select(p => new PersonViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade
            }).ToList();
        }
    }
}
