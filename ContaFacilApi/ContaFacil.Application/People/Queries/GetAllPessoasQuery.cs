using MediatR;
using ContaFacil.Application.People.ViewModels;

namespace ContaFacil.Application.People.Queries
{
    public record GetAllPessoasQuery : IRequest<List<PessoaViewModel>>;
}
