using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using MediatR;

namespace ContaFacil.Application.People.Commands
{
    public class CreatePersonCommandHandler
        : IRequestHandler<CreatePessoaCommand, Guid>
    {
        private readonly IPersonRepository _repository;

        //Injetando dependencias
        public CreatePersonCommandHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        //Task para criar a pessoa no banco de dados
        public async Task<Guid> Handle(CreatePessoaCommand request, CancellationToken cancellationToken)
        {
            //Cria a entidade pessoa
            var pessoa = new Person
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Idade = request.Idade,
                CreatedAt = DateTime.UtcNow,
                Ativo = 1
            };

            //Adiciona a entidade pessoa ao banco de dados
            await _repository.AddAsync(pessoa);

            //Retorna o id criado
            return pessoa.Id;
        }

    }
}
