using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using MediatR;

namespace ContaFacil.Application.People.Commands
{
    public class CreatePessoaCommandHandler
        : IRequestHandler<CreatePessoaCommand, Guid>
    {
        private readonly IPessoaRepository _repository;

        public CreatePessoaCommandHandler(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            CreatePessoaCommand request,
            CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Idade = request.Idade,
                CreatedAt = DateTime.UtcNow,
                Ativo = 1
            };

            await _repository.AddAsync(pessoa);

            return pessoa.Id;
        }

    }
}
