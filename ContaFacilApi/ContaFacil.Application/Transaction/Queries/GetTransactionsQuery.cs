using MediatR;

namespace ContaFacil.Application.Transactions.Queries
{
    public record GetTransactionsQuery : IRequest<List<TransactionViewModel>>;
}
