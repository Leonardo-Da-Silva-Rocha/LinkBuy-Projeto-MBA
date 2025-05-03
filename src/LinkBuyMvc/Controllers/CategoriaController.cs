using Microsoft.AspNetCore.Mvc;
using LinkBuyLibrary.Services;
using LinkBuyLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LinkBuyMvc.Controllers
{
    [Authorize]
    [Route("categorias")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCategoriasAsync());
        }

        [HttpGet("gerenciar/nova-categoria")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("gerenciar/nova-categoria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateCategoriaAsync(categoria);

                if (result > 0)
                {
                    TempData["CategoriaMsgSucesso"] = "Categoria criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                return View(categoria);
            }
            return View(categoria);
        }

        [HttpGet("gerenciar/deletar-categoria")]
        public async Task<IActionResult> Delete(int id)
        {

            var categoria = await _service.GetCategoriasByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost("gerenciar/deletar-categoria"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);
            try
            {
                int resultado = 0;
                if (categoria != null)
                {
                    resultado = await _service.DeleteCategoriaAsync(categoria);
                }

                if (resultado > 0)
                {
                    TempData["CategoriaMsgSucesso"] = "Categoria excluida com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                    
                return View(categoria);

            }
            catch (DbUpdateException ex)
            {
                ViewData["MensagemErro"] = "Não é possível excluir uma categoria com produtos associados.";
                return View(categoria);
            }
            catch (Exception ex)
            {
                ViewData["MensagemErro"] = "Ocorreu um erro inesperado.";
                return View(categoria);
            }

        }

        [HttpGet("gerenciar/editar-categoria")]
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost("gerenciar/editar-categoria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Categoria categoria)
        {
            var resultado = 0;

            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                resultado = await _service.UpdateCategoriaAsync(categoria);

                if (resultado > 0)
                {
                    TempData["CategoriaMsgSucesso"] = "Categoria editada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                    
                return View(categoria);
            }

            return View(categoria);
        }

        [HttpGet("gerenciar/detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }
    }
}