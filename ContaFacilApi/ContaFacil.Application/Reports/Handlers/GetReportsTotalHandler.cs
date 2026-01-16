using MediatR;
using ContaFacil.Application.Reports.Response;
using ContaFacil.Application.Common.Interface;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.Relatorios.Queries
{
    public class ConsultaTotaisQueryHandler
        : IRequestHandler<GetReportsTotalsQuery, GetTotalsResponse>
    {
        private readonly IReportsRepository _repository;
        private readonly ILogger<ConsultaTotaisQueryHandler> _logger;

        public ConsultaTotaisQueryHandler(
            IReportsRepository repository,
            ILogger<ConsultaTotaisQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetTotalsResponse> Handle(
            GetReportsTotalsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando busca de relatório de totais");

                var pessoas = await _repository.ObterTotaisPorPessoaAsync();
                var totalGeral = await _repository.ObterTotaisGeralAsync();

                _logger.LogInformation(
                    "Relatório de totais gerado com sucesso. Total de pessoas: {Count}, TotalGeral: {TotalGeral}",
                    pessoas?.Count ?? 0,
                    totalGeral?.TotalDespesas ?? 0);

                return new GetTotalsResponse
                {
                    Pessoas = pessoas,
                    TotalGeral = totalGeral
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar relatório de totais");
                throw;
            }
        }
    }
}
