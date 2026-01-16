using MediatR;
using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.Transactions.Commands
{
    public class CreateTransactionCommandHandler
        : IRequestHandler<CreateTransactionCommand>
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<CreateTransactionCommandHandler> _logger;

        //Injeção de dependencia
        public CreateTransactionCommandHandler(
            ITransactionRepository repository,
            ILogger<CreateTransactionCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Task para criar o objeto de transações
        public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando criação de transação. PessoaId: {PessoaId}, CategoryId: {CategoryId}, Valor: {Valor}, TipoId: {TipoId}",
                    request.PessoaId, request.CategoryId, request.Valor, request.TipoId);

                // Validação de valor
                if (request.Valor <= 0)
                {
                    _logger.LogWarning("Tentativa de criar transação com valor inválido: {Valor}", request.Valor);
                    throw new ArgumentException("Valor deve ser positivo.", nameof(request.Valor));
                }

                // Buscar pessoa
                var pessoa = await _repository.GetPessoaAsync(request.PessoaId);
                if (pessoa == null)
                {
                    _logger.LogWarning("Tentativa de criar transação para pessoa não encontrada. PessoaId: {PessoaId}", request.PessoaId);
                    throw new KeyNotFoundException($"Pessoa com ID {request.PessoaId} não encontrada.");
                }

                // Buscar categoria
                var categoria = await _repository.GetCategoryAsync(request.CategoryId);
                if (categoria == null)
                {
                    _logger.LogWarning("Tentativa de criar transação com categoria não encontrada. CategoryId: {CategoryId}", request.CategoryId);
                    throw new KeyNotFoundException($"Categoria com ID {request.CategoryId} não encontrada.");
                }

                // Se for menor de idade não pode adicionar receita
                if (pessoa.Idade < 18 && request.TipoId == 2)
                {
                    _logger.LogWarning(
                        "Tentativa de criar receita para menor de idade. PessoaId: {PessoaId}, Idade: {Idade}",
                        request.PessoaId, pessoa.Idade);
                    throw new InvalidOperationException("Menor de idade não pode registrar receita.");
                }

                // Só adicionar a categoria se bater com a finalidade cadastrada
                var finalidade = categoria.Purpose.Id;

                var categoriaValida =
                    finalidade == 3 ||
                    (finalidade == 1 && request.TipoId == 1) ||
                    (finalidade == 2 && request.TipoId == 2);

                if (!categoriaValida)
                {
                    _logger.LogWarning(
                        "Tentativa de criar transação com categoria incompatível. CategoryId: {CategoryId}, Finalidade: {Finalidade}, TipoId: {TipoId}",
                        request.CategoryId, finalidade, request.TipoId);
                    throw new InvalidOperationException("Categoria incompatível com o tipo da transação.");
                }

                //Cria o objeto se todas validações forem ok
                var transaction = new Transaction
                {
                    Descricao = request.Descricao,
                    Valor = request.Valor,
                    TipoId = request.TipoId,
                    CategoryId = request.CategoryId,
                    PessoaId = request.PessoaId
                };

                //Salva no banco de dados
                await _repository.AddAsync(transaction);

                _logger.LogInformation(
                    "Transação criada com sucesso. PessoaId: {PessoaId}, Valor: {Valor}, TipoId: {TipoId}",
                    request.PessoaId, request.Valor, request.TipoId);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao criar transação. Valor: {Valor}", request.Valor);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Recurso não encontrado ao criar transação. PessoaId: {PessoaId}, CategoryId: {CategoryId}",
                    request.PessoaId, request.CategoryId);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Operação inválida ao criar transação. PessoaId: {PessoaId}", request.PessoaId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Erro ao criar transação. PessoaId: {PessoaId}, CategoryId: {CategoryId}, Valor: {Valor}",
                    request.PessoaId, request.CategoryId, request.Valor);
                throw;
            }
        }
    }
}
