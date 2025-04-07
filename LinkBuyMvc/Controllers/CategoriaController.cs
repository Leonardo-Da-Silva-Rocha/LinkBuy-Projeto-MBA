using Microsoft.AspNetCore.Mvc;
using LinkBuyLibrary.Services;
using LinkBuyLibrary.Models;

namespace LinkBuyMvc.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCategoriasAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateCategoriaAsync(categoria);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                return View(categoria);
            }
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var categoria = await _service.GetCategoriasByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);
            int resultado = 0;
            if (categoria != null)
            {
                resultado = await _service.DeleteCategoriaAsync(categoria);
            }

            if (resultado > 0)
                return RedirectToAction(nameof(Index));

            return View(categoria);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
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
                
                if(resultado > 0) 
                    return RedirectToAction(nameof(Index));
                
                return View(categoria);
            }

            return View(categoria);
        }

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