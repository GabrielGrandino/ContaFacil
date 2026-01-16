using MediatR;
using ContaFacil.Application.Reports.Response;
using ContaFacil.Application.Common.Interface;

namespace ContaFacil.Application.Relatorios.Queries
{
    public class ConsultaTotaisQueryHandler
        : IRequestHandler<GetReportsTotalsQuery, GetTotalsResponse>
    {
        private readonly IReportsRepository _repository;

        public ConsultaTotaisQueryHandler(IReportsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetTotalsResponse> Handle(
            GetReportsTotalsQuery request,
            CancellationToken cancellationToken)
        {
            var pessoas = await _repository.ObterTotaisPorPessoaAsync();
            var totalGeral = await _repository.ObterTotaisGeralAsync();

            return new GetTotalsResponse
            {
                Pessoas = pessoas,
                TotalGeral = totalGeral
            };
        }
    }
}
