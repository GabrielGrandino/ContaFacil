using MediatR;
using ContaFacil.Application.People.Commands;
using ContaFacil.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContaFacil.Application.People.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionRepository _transacaoRepository;
        private readonly ILogger<DeletePersonCommandHandler> _logger;

        //Injeção de dependencias
        public DeletePersonCommandHandler(
            IPersonRepository personRepository,
            ITransactionRepository transacaoRepository,
            ILogger<DeletePersonCommandHandler> logger)
        {
            _personRepository = personRepository;
            _transacaoRepository = transacaoRepository;
            _logger = logger;
        }

        //Cria a tarefa para deletar a pessoa do banco de dados
        public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando exclusão de pessoa. PessoaId: {PessoaId}",
                    request.PessoaId);

                //Captura a pessoa pelo id informado
                var pessoa = await _personRepository.GetByIdAsync(request.PessoaId);

                if (pessoa is null)
                {
                    _logger.LogWarning(
                        "Tentativa de excluir pessoa não encontrada. PessoaId: {PessoaId}",
                        request.PessoaId);
                    throw new KeyNotFoundException($"Pessoa com ID {request.PessoaId} não encontrada.");
                }

                _logger.LogInformation(
                    "Excluindo pessoa. PessoaId: {PessoaId}, Nome: {Nome}",
                    request.PessoaId, pessoa.Nome);

                // Deleta a pessoa
                await _personRepository.DeleteAsync(pessoa);

                _logger.LogInformation(
                    "Pessoa excluída com sucesso. PessoaId: {PessoaId}, Nome: {Nome}",
                    request.PessoaId, pessoa.Nome);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Pessoa não encontrada para exclusão. PessoaId: {PessoaId}", request.PessoaId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Erro ao excluir pessoa. PessoaId: {PessoaId}",
                    request.PessoaId);
                throw;
            }
        }
    }
}
