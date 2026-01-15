using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Common.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task<List<Category>> GetAllAsync();
        Task<bool> PurposeExistsAsync(int purposeId);
    }
}
