using LinkBuyLibrary.Data;
using LinkBuyLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkBuyLibrary.Services
{
    public class VendedorService
    {
        public AppDbContext _dbContext;

        public VendedorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateVendedorAsync(Vendedor vendedor)
        {
            await _dbContext.Vendedores.AddAsync(vendedor);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vendedor>> GetAllVendedoresAsync()
        {
            return await _dbContext.Vendedores.ToListAsync();
        }

        public async Task<Vendedor?> GetVendedorByIdAsync(int id)
        {
            return await _dbContext.Vendedores.FindAsync(id);
        }

        public async Task<int> DeleteVendedorAsync(Vendedor vendedor)
        {
            _dbContext.Remove(vendedor);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateVendedorAsync(Vendedor vendedor)
        {
            _dbContext.Update(vendedor);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
