using MediatR;
using ContaFacil.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.Categories.Queries
{
    public class GetCategoriesQueryHandler
        : IRequestHandler<GetCategoriesQuery, List<CategoryViewModel>>
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<GetCategoriesQueryHandler> _logger;

        //Injeção de dependencia
        public GetCategoriesQueryHandler(
            ICategoryRepository repository,
            ILogger<GetCategoriesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Tarefa para listar todas as categorias
        public async Task<List<CategoryViewModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando busca de todas as categorias");

                //Captura as categorias
                var categories = await _repository.GetAllAsync();

                //Retorna a lista de categorias
                var viewModels = categories.Select(c =>
                    new CategoryViewModel(
                        c.Id,
                        c.Descricao,
                        c.Purpose.Descricao
                    )).ToList();

                return viewModels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as categorias");
                throw;
            }
        }
    }
}
