using MediatR;
using ContaFacil.Application.Reports.Response;

namespace ContaFacil.Application.Relatorios.Queries
{
    public record GetReportsTotalsQuery() : IRequest<GetTotalsResponse>;
}
