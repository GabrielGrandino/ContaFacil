using MediatR;

namespace ContaFacil.Application.Categories.Commands
{
    public record CreateCategoryCommand(string Descricao, int PurposeId) : IRequest;
}
