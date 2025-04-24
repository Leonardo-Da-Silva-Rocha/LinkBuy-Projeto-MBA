using LinkBuyLibrary.Data;
using LinkBuyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace LinkBuyLibrary.Services
{
    public class ProdutoService
    {
        protected readonly AppDbContext _context;

        public ProdutoService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            return await _context.Produtos.Include(p => p.Categoria).Include(v => v.Vendedor).ToListAsync();
        }

        public async Task CreateImage(IFormFile ImagemUpload, string nomeArquivo)
        {
            try
            {
                if (ImagemUpload != null && ImagemUpload.Length > 0)
                {
                    var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(caminhoPasta))
                        Directory.CreateDirectory(caminhoPasta);

                    var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await ImagemUpload.CopyToAsync(stream);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Ocorreu um erro ao salvar a imagem");
            }
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosByVendedor(int id)
        {
            return await _context.Produtos.Where(p => p.VendedorId == id).Include(p => p.Categoria).Include(p => p.Vendedor).AsNoTracking().ToListAsync();
        }

        public async Task<Produto?> GetDetalheProduto(int id)
        {
            return await _context.Produtos.Include(p => p.Categoria).Include(p => p.Vendedor).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> CreateProdutoAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditProdutoAsync(Produto produto)
        {
            _context.Update(produto);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteProdutoAsync(Produto produto)
        {
            _context.Remove(produto);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteImage(string imageName)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", imageName);
                
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch
            {
                throw new Exception("Erro ao deletar a imagem do produto");
            }
        }

    }
}
