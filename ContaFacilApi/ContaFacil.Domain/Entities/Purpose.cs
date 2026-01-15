namespace ContaFacil.Domain.Entities
{
    public class Purpose
    {
        public int Id { get; set; } // 1, 2, 3
        public string Descricao { get; set; } = null!;

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
