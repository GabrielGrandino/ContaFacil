using ContaFacil.Application.Common.Interfaces;
using ContaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContaFacil.Infrastructure.Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ContaFacilDbContext _context;

        //Injetando o context
        public CategoryRepository(ContaFacilDbContext context)
        {
            _context = context;
        }

        //Task para adicionar objeto categorias
        public async Task AddAsync(Category category)
        {
            _context.Categorias.Add(category);
            await _context.SaveChangesAsync();
        }

        //Task para listar objetos
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categorias
                .Include(c => c.Purpose)
                .ToListAsync();
        }

        //Task para verificar se tem finalidade
        public async Task<bool> PurposeExistsAsync(int purposeId)
        {
            return await _context.Finalidade.AnyAsync(p => p.Id == purposeId);
        }
    }
}
