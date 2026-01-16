namespace ContaFacil.Application.Transactions.Queries
{
    public record TransactionViewModel(
        int Id,
        string Descricao,
        decimal Valor,
        string Tipo,
        string Categoria,
        string Pessoa
    );
}
