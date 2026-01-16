using MediatR;
using ContaFacil.Application.People.Queries;
using ContaFacil.Application.People.ViewModels;
using ContaFacil.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.People.Handlers
{
    public class GetAllPersonsQueryHandler
        : IRequestHandler<GetAllPersonsQuery, List<PersonViewModel>>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetAllPersonsQueryHandler> _logger;

        //Injeção de dependencias
        public GetAllPersonsQueryHandler(
            IPersonRepository repository,
            ILogger<GetAllPersonsQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<PersonViewModel>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando busca de todas as pessoas");

                //Cria uma lista de pessoas
                var pessoas = await _repository.GetAllAsync();

                //Retorna a lista pelo viewmodel
                var viewModels = pessoas.Select(p => new PersonViewModel
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Idade = p.Idade
                }).ToList();

                return viewModels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as pessoas");
                throw;
            }
        }
    }
}
