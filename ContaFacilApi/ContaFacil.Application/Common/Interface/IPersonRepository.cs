using ContaFacil.Domain.Entities;

namespace ContaFacil.Application.Common.Interfaces
{
    public interface IPersonRepository
    {
        Task AddAsync(Person pessoa);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(Guid id);
        Task DeleteAsync(Person pessoa);
    }
}
