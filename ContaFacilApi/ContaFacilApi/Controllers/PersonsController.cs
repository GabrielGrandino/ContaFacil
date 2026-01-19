using ContaFacil.Application.People.Commands;
using ContaFacil.Application.People.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContaFacil.API.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar todas as operações de CRUD para pessoas
    /// </summary>
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova pessoa no sistema
        /// </summary>
        /// <param name="command">Dados necessários para criar uma pessoa</param>
        /// <returns>Retorna o ID único (Guid) da pessoa criada</returns>
        /// <remarks>
        /// Exemplo de request:
        /// 
        /// POST /api/pessoas
        /// {
        ///     "nome": "João da Silva",
        ///     "idade": 20
        /// }
        /// </remarks>
        /// <response code="200">Retorna o id da pessoa criada</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreatePessoaCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Lista todas as pessoas cadastradas no sistema
        /// </summary>
        /// <returns>Retorna uma lista com todas as pessoas</returns>
        /// <remarks>
        /// Este endpoint retorna todas as pessoas ativas e inativas.
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
        /// <response code="200">Retorna a lista de pessoas</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllPersonsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Remove uma pessoa do sistema pelo seu ID
        /// </summary>
        /// <param name="id">ID da pessoa a ser removida (Guid)</param>
        /// <returns>Retorna status 204 (No Content) em caso de sucesso</returns>
        /// <remarks>
        /// Exemplo de request:
        /// 
        /// DELETE /api/pessoas/123e4567-e89b-12d3-a456-426614174000
        /// 
        /// ⚠️ **Atenção:** Esta operação é irreversível. Ao excluir uma pessoa todas as suas transações também serão excluídas.
        /// </remarks>
        /// <response code="204">Pessoa removida com sucesso</response>
        /// <response code="404">Pessoa não encontrada com o ID fornecido</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePersonCommand(id));
            return NoContent();
        }
    }
}