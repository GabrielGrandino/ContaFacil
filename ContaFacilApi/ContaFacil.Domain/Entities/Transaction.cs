namespace ContaFacil.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }

        // 1 - despesa / 2 - receita / 3 - Ambos
        public int TipoId { get; set; }
        public Purpose Finalidade { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Categoria { get; set; } = null!;

        public Guid PessoaId { get; set; }
        public Person Pessoa { get; set; } = null!;
    }
}
