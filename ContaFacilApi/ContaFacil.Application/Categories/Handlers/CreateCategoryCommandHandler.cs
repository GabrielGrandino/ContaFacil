using MediatR;
using ContaFacil.Application.Categories.Commands;
using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        //Injeção de dependencia
        public CreateCategoryCommandHandler(
            ICategoryRepository repository,
            ILogger<CreateCategoryCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Tarefa para criar categoria no banco de dados
        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando criação de categoria. Descricao: {Descricao}, PurposeId: {PurposeId}",
                    request.Descricao, request.PurposeId);

                //Validação básica
                if (string.IsNullOrWhiteSpace(request.Descricao))
                {
                    _logger.LogWarning("Tentativa de criar categoria com descrição vazia ou nula");
                    throw new ArgumentException("Descrição não pode ser vazia ou nula.", nameof(request.Descricao));
                }

                //Validação de finalidade
                if (!await _repository.PurposeExistsAsync(request.PurposeId))
                {
                    _logger.LogWarning("Tentativa de criar categoria com finalidade inválida. PurposeId: {PurposeId}", request.PurposeId);
                    throw new KeyNotFoundException($"Finalidade com ID {request.PurposeId} não encontrada.");
                }

                //Cria objeto de categoria
                var category = new Category
                {
                    Descricao = request.Descricao,
                    PurposeId = request.PurposeId
                };

                //Adiciona o objeto ao banco de dados
                await _repository.AddAsync(category);

                _logger.LogInformation(
                    "Categoria criada com sucesso. Descricao: {Descricao}, PurposeId: {PurposeId}",
                    request.Descricao, request.PurposeId);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao criar categoria. Descricao: {Descricao}", request.Descricao);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Finalidade não encontrada ao criar categoria. PurposeId: {PurposeId}", request.PurposeId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Erro ao criar categoria. Descricao: {Descricao}, PurposeId: {PurposeId}",
                    request.Descricao, request.PurposeId);
                throw;
            }
        }
    }
}
