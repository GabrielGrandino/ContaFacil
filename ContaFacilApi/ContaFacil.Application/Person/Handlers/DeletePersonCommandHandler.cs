using MediatR;
using ContaFacil.Application.People.Commands;
using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Application.People.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionRepository _transacaoRepository;

        public DeletePersonCommandHandler(
            IPersonRepository personRepository,
            ITransactionRepository transacaoRepository)
        {
            _personRepository = personRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var pessoa = await _personRepository.GetByIdAsync(request.PessoaId);

            if (pessoa is null)
                throw new Exception("Pessoa não encontrada.");

            // Apaga transações primeiro (regra clara)
            await _transacaoRepository.DeleteByPessoaIdAsync(request.PessoaId);

            // Apaga a pessoa
            await _personRepository.DeleteAsync(pessoa);
        }
    }
}
