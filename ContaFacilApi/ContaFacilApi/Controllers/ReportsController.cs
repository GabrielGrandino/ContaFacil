using MediatR;
using Microsoft.AspNetCore.Mvc;
using ContaFacil.Application.Relatorios.Queries;

namespace ContaFacil.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Traz o relatorios de despesas por pessoas e geral
        /// </summary>
        /// <returns>Retorna um relatorio de despesas por pessoas e geral</returns>
        /// <remarks>
        /// Este endpoint retorna um relatorio de despesas por pessoas e geral
        /// 
        /// Exemplo de resposta:
        /// [
        ///     {
        ///         "id": "123e4567-e89b-12d3-a456-426614174000",
        ///         "nome": "João da Silva",
        ///         "idade": 20,
        ///     }
        /// ]
        /// </remarks>
        /// <response code="200">Retorna um relatorio de despesas</response>
        [HttpGet("totais")]
        public async Task<IActionResult> GetTotais()
        {
            var result = await _mediator.Send(new GetReportsTotalsQuery());
            return Ok(result);
        }
    }
}
