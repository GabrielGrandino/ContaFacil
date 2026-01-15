using ContaFacil.Application.Categories.Commands;
using ContaFacil.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria uma nova categoria no sistema
    /// </summary>
    /// <param name="command">Dados necessários para criar uma categoria</param>
    /// <returns>Retorna o ID único da categoria criada</returns>
    /// <remarks>
    /// Exemplo de request:
    /// 
    /// POST /api/pessoas
    /// {
    ///     "descriçao": "Assinaturas",
    ///     "finalidade": 1
    /// }
    /// 1 - despesa; 2 - receita; 3 - ambas
    /// </remarks>
    /// <response code="200">Retorna o id da categoria criada</response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Lista todas as categorias cadastradas no sistema
    /// </summary>
    /// <returns>Retorna uma lista com todas as categorias</returns>
    /// <remarks>
    /// Este endpoint retorna todas as categorias ativas e inativas.
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
    /// <response code="200">Retorna a lista de categorias</response>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }
}
