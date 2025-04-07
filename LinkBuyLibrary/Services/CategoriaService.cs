using LinkBuyLibrary.Data;
using LinkBuyLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkBuyLibrary.Services
{
    public class CategoriaService
    {
        public AppDbContext _dbContext;

        public CategoriaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateCategoriaAsync(Categoria categoria)
        {
            await _dbContext.Categorias.AddAsync(categoria);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync()
        {
            return await _dbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetCategoriasByIdAsync(int id)
        {
            return await _dbContext.Categorias.FindAsync(id);
        }

        public async Task<int> DeleteCategoriaAsync(Categoria categoria)
        {
            _dbContext.Remove(categoria);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateCategoriaAsync(Categoria categoria)
        {
            _dbContext.Update(categoria);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}