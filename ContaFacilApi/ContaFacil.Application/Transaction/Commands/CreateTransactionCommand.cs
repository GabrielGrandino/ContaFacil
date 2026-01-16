using MediatR;

namespace ContaFacil.Application.Transactions.Commands
{
    public record CreateTransactionCommand(string Descricao, decimal Valor, int TipoId, int CategoryId, Guid PessoaId) : IRequest;
}
