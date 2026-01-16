using MediatR;
using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Transactions.Commands
{
    public class CreateTransactionCommandHandler
        : IRequestHandler<CreateTransactionCommand>
    {
        private readonly ITransactionRepository _repository;

        //Injeção de dependencia
        public CreateTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        //Task para criar o objeto de transações
        public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.Valor <= 0)
                throw new Exception("Valor deve ser positivo.");

            var pessoa = await _repository.GetPessoaAsync(request.PessoaId)
                ?? throw new Exception("Pessoa não encontrada.");

            var categoria = await _repository.GetCategoryAsync(request.CategoryId)
                ?? throw new Exception("Categoria não encontrada.");

            // Se for menor de idade não pode adicionar receita
            if (pessoa.Idade < 18 && request.TipoId == 2)
                throw new Exception("Menor de idade não pode registrar receita.");

            // Só adicionar a categoria se bater com a finalidade cadastrada
            var finalidade = categoria.Purpose.Id;

            var categoriaValida =
                finalidade == 3 ||
                (finalidade == 1 && request.TipoId == 1) ||
                (finalidade == 2 && request.TipoId == 2);

            if (!categoriaValida)
                throw new Exception("Categoria incompatível com o tipo da transação.");

            //Cria o objeto se todas validações forem ok
            var transaction = new Transaction
            {
                Descricao = request.Descricao,
                Valor = request.Valor,
                TipoId = request.TipoId,
                CategoryId = request.CategoryId,
                PessoaId = request.PessoaId
            };

            //Salva no banco de dados
            await _repository.AddAsync(transaction);
        }
    }
}
