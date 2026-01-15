using MediatR;

namespace ContaFacil.Application.People.Commands
{
    public record DeletePersonCommand(Guid PessoaId) : IRequest;
}
