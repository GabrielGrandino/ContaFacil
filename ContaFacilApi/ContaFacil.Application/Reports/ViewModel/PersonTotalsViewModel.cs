using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Application.Reports.DTOs
{
    public class PersonTotalsViewModel
    {
        public Guid PessoaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal TotalDespesas { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal Saldo { get; set; }
    }
}
