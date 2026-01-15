using MediatR;
using ContaFacil.Application.People.Commands;
using ContaFacil.Application.Common.Interfaces;

namespace ContaFacil.Application.People.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionRepository _transacaoRepository;

        //Injeção de dependencias
        public DeletePersonCommandHandler(
            IPersonRepository personRepository,
            ITransactionRepository transacaoRepository)
        {
            _personRepository = personRepository;
            _transacaoRepository = transacaoRepository;
        }

        //Cria a tarefa para deletar a pessoa do banco de dados
        public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            //Captura a pessoa pelo id informado
            var pessoa = await _personRepository.GetByIdAsync(request.PessoaId);

            if (pessoa is null)
                throw new Exception("Pessoa não encontrada.");

            // Deleta as transações primeiro
            await _transacaoRepository.DeleteByPessoaIdAsync(request.PessoaId);

            // Deleta a pessoa
            await _personRepository.DeleteAsync(pessoa);
        }
    }
}
