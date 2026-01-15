using ContaFacil.Application.People.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContaFacil.API.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePessoaCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}
