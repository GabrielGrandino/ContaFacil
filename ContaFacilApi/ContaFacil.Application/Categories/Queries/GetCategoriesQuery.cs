using MediatR;

namespace ContaFacil.Application.Categories.Queries
{
    public record GetCategoriesQuery : IRequest<List<CategoryViewModel>>;
}
