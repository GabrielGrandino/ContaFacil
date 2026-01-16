using ContaFacil.Application.Reports.DTOs;
using ContaFacil.Application.Reports.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaFacil.Application.Reports.Response
{
    public class GetTotalsResponse
    {
        public List<PersonTotalsViewModel> Pessoas { get; set; } = [];
        public GlobalTotalsViewModel? TotalGeral { get; set; }
    }
}
