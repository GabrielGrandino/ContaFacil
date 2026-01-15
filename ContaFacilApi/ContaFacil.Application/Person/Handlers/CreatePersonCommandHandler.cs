using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using MediatR;

namespace ContaFacil.Application.People.Commands
{
    public class CreatePersonCommandHandler
        : IRequestHandler<CreatePessoaCommand, Guid>
    {
        private readonly IPersonRepository _repository;

        public CreatePersonCommandHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            CreatePessoaCommand request,
            CancellationToken cancellationToken)
        {
            var pessoa = new Person
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
