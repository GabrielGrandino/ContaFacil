using MediatR;
using ContaFacil.Application.Categories.Commands;
using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        //Injeção de dependencia
        public CreateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        //Tarefa para criar categoria no banco de dados
        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.PurposeExistsAsync(request.PurposeId))
                throw new Exception("Finalidade inválida.");

            //Cria objeto de categoria
            var category = new Category
            {
                Descricao = request.Descricao,
                PurposeId = request.PurposeId
            };

            //Adiciona o objeto ao banco de dados
            await _repository.AddAsync(category);
        }
    }
}
