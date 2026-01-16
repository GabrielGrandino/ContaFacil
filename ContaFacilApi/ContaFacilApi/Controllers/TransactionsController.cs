using ContaFacil.Application.Transactions.Commands;
using ContaFacil.Application.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria uma nova transação no sistema
    /// </summary>
    /// <param name="command">Dados necessários para criar uma transação</param>
    /// <returns>Retorna o ID único da transação criada</returns>
    /// <remarks>
    /// Exemplo de request:
    /// 
    /// POST /api/pessoas
    /// {
    ///     "descriçao": "Assinaturas",
    ///     "finalidade": 1
    /// }
    /// </remarks>
    /// <response code="200">Retorna o id da transação criada</response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Lista todas as transações cadastradas no sistema
    /// </summary>
    /// <returns>Retorna uma lista com todas as transações</returns>
    /// <remarks>
    /// Este endpoint retorna todas as transações do sistema
    /// 
    /// Exemplo de resposta:
    /// [
    ///     {
    ///         "id": 1,
    ///         "descrição": "Assinatura",
    ///         "finalidade": 1,
    ///     }
    /// ]
    /// </remarks>
    /// <response code="200">Retorna a lista de transações</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetTransactionsQuery());
        return Ok(result);
    }
}
