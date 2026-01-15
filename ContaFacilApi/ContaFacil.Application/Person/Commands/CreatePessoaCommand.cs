using MediatR;

namespace ContaFacil.Application.People.Commands
{
    public class CreatePessoaCommand : IRequest<Guid>
    {
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
    }
}