using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Application.Reports.ViewModel
{
    public class GlobalTotalsViewModel
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal SaldoGeral { get; set; }
    }
}
