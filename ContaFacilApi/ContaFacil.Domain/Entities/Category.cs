namespace ContaFacil.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        // FK
        public int PurposeId { get; set; }

        // Navegação
        public Purpose Purpose { get; set; } = null!;
    }
}
