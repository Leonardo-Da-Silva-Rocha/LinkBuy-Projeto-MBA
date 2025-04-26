using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LinkBuyLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using LinkBuyLibrary.Services;
using System.Security.Claims;

namespace LinkBuyMvc.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;
        private readonly CategoriaService _serviceCategoria;
        private readonly VendedorService _vendedorService;

        public ProdutoController(ProdutoService service, CategoriaService serviceCategoria, VendedorService vendedorService)
        {
            _service = service;
            _serviceCategoria = serviceCategoria;
            _vendedorService = vendedorService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var vendedor = await _vendedorService.GetVendedorByIdLoginAsync(userIdString);

            return View(await _service.GetAllProdutosByVendedor(vendedor.Id));
        }


        public async Task<IActionResult> Details(int id)
        {
            var produto = await _service.GetDetalheProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["CategoriaId"] = new SelectList(await _serviceCategoria.GetAllCategoriasAsync(), "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Valor,Estoque,ImagemUpload,CategoriaId")] Produto produto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var vendedor = await _vendedorService.GetVendedorByIdLoginAsync(userIdString);

                    if (vendedor == null) return View(produto);

                    produto.Vendedor = vendedor;

                    string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(produto.ImagemUpload.FileName);

                    await _service.CreateImage(produto.ImagemUpload, nomeArquivo);

                    produto.Imagem = nomeArquivo;

                    var result = await _service.CreateProdutoAsync(produto);

                    if (result > 0)
                    {
                        TempData["ProdutoMsgSucesso"] = "Produto criado com sucesso!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewData["CategoriaId"] = new SelectList(await _serviceCategoria.GetAllCategoriasAsync(), "Id", "Descricao", produto.CategoriaId);

            return View(produto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _service.GetDetalheProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            ViewData["CategoriaId"] = new SelectList(await _serviceCategoria.GetAllCategoriasAsync(), "Id", "Descricao");
            ViewData["VendedorId"] = produto.VendedorId;
            return View(produto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Valor,Estoque,ImagemUpload,CategoriaId,VendedorId")] Produto produto)
        {

            if (ModelState.IsValid)
            {
                var produtoEdit = await _service.GetDetalheProduto(id);

                if (produtoEdit != null)
                {
                    await _service.DeleteImage(produtoEdit.Imagem);
                }

                string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(produto.ImagemUpload.FileName);
                produto.Imagem = nomeArquivo;
                await _service.CreateImage(produto.ImagemUpload, produto.Imagem);

                var result = await _service.EditProdutoAsync(produto);
                if (result > 0)
                {
                    TempData["ProdutoMsgSucesso"] = "Produto editado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
               
            }
            
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _service.GetDetalheProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _service.GetDetalheProduto(id);
            if (produto == null)
            {
                ModelState.AddModelError(string.Empty, "Erro o produto não existe");
                return View();
            }

            var result = await _service.DeleteProdutoAsync(produto);

            if (result > 0)
            {
                await _service.DeleteImage(produto.Imagem);
                TempData["ProdutoMsgSucesso"] = "Produto excluido com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Erro o produto não existe");
            return View();

        }
    }
}
