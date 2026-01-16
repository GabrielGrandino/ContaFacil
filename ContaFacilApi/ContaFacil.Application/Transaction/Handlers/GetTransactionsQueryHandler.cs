using MediatR;
using ContaFacil.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.Transactions.Queries
{
    public class GetTransactionsQueryHandler
        : IRequestHandler<GetTransactionsQuery, List<TransactionViewModel>>
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<GetTransactionsQueryHandler> _logger;

        //Injeção de dependencia
        public GetTransactionsQueryHandler(
            ITransactionRepository repository,
            ILogger<GetTransactionsQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Task para listar todas as tarefas do bd
        public async Task<List<TransactionViewModel>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando busca de todas as transações");

                var transactions = await _repository.GetAllAsync();

                _logger.LogInformation(
                    "Busca concluída. Total de transações encontradas: {Count}",
                    transactions.Count);

                var viewModels = transactions.Select(t =>
                    new TransactionViewModel(
                        t.Id,
                        t.Descricao,
                        t.Valor,
                        t.TipoId == 1 ? "Despesa" : "Receita",
                        t.Categoria.Descricao,
                        t.Pessoa.Nome
                    )).ToList();

                return viewModels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as transações");
                throw;
            }
        }
    }
}
