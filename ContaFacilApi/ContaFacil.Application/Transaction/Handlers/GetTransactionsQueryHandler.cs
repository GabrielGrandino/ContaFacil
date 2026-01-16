using MediatR;
using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Application.Transactions.Queries
{
    public class GetTransactionsQueryHandler
        : IRequestHandler<GetTransactionsQuery, List<TransactionViewModel>>
    {
        private readonly ITransactionRepository _repository;

        //Injeção de dependencia
        public GetTransactionsQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        //Task para listar todas as tarefas do bd
        public async Task<List<TransactionViewModel>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _repository.GetAllAsync();

            return transactions.Select(t =>
                new TransactionViewModel(
                    t.Id,
                    t.Descricao,
                    t.Valor,
                    t.TipoId == 1 ? "Despesa" : "Receita",
                    t.Categoria.Descricao,
                    t.Pessoa.Nome
                )).ToList();
        }
    }
}
