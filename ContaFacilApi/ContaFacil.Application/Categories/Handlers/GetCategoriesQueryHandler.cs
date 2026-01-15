using MediatR;
using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Application.Categories.Queries
{
    public class GetCategoriesQueryHandler
        : IRequestHandler<GetCategoriesQuery, List<CategoryViewModel>>
    {
        private readonly ICategoryRepository _repository;

        //Injeção de dependencia
        public GetCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        //Tarefa para listar todas as categorias
        public async Task<List<CategoryViewModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            //Captura as categorias
            var categories = await _repository.GetAllAsync();

            //Retorna a lista de categorias
            return categories.Select(c =>
                new CategoryViewModel(
                    c.Id,
                    c.Descricao,
                    c.Purpose.Descricao
                )).ToList();
        }
    }
}
