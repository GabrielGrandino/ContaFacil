using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Ativo { get; set; }
    }
}
