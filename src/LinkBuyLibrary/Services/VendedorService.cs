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

        public async Task<Vendedor?> GetVendedorByIdLoginAsync(string idLogin)
        {
            return await _dbContext.Vendedores.FirstOrDefaultAsync(v => v.FkLogin == idLogin);
        }

        public async Task<Vendedor?> GetVendedorByIdAsync(int id)
        {
            return await _dbContext.Vendedores.FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
