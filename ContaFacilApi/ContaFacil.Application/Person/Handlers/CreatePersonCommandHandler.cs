using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.People.Commands
{
    public class CreatePersonCommandHandler
        : IRequestHandler<CreatePessoaCommand, Guid>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        //Injetando dependencias
        public CreatePersonCommandHandler(
            IPersonRepository repository,
            ILogger<CreatePersonCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Task para criar a pessoa no banco de dados
        public async Task<Guid> Handle(CreatePessoaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando criação de pessoa. Nome: {Nome}, Idade: {Idade}",
                    request.Nome, request.Idade);

                //Validação básica
                if (string.IsNullOrWhiteSpace(request.Nome))
                {
                    _logger.LogWarning("Tentativa de criar pessoa com nome vazio ou nulo");
                    throw new ArgumentException("Nome não pode ser vazio ou nulo.", nameof(request.Nome));
                }

                if (request.Idade <= 0 || request.Idade > 150)
                {
                    _logger.LogWarning("Tentativa de criar pessoa com idade inválida: {Idade}", request.Idade);
                    throw new ArgumentException("Idade deve estar entre 1 e 150 anos.", nameof(request.Idade));
                }

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

                _logger.LogInformation(
                    "Pessoa criada com sucesso. Id: {PersonId}, Nome: {Nome}",
                    pessoa.Id, pessoa.Nome);

                //Retorna o id criado
                return pessoa.Id;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao criar pessoa. Nome: {Nome}", request.Nome);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Erro ao criar pessoa. Nome: {Nome}, Idade: {Idade}",
                    request.Nome, request.Idade);
                throw;
            }
        }
    }
}
