using ContaFacil.Application.Reports.DTOs;
using ContaFacil.Application.Reports.ViewModel;

namespace ContaFacil.Application.Common.Interface
{
    public interface IReportsRepository
    {
        Task<List<PersonTotalsViewModel>> ObterTotaisPorPessoaAsync();
        Task<GlobalTotalsViewModel?> ObterTotaisGeralAsync();
    }
}
