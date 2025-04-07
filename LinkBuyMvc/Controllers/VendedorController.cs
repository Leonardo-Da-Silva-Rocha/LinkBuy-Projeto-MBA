using Microsoft.AspNetCore.Mvc;
using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;

namespace LinkBuyMvc.Controllers
{
    public class VendedorController : Controller
    {
        private readonly VendedorService _service;

        public VendedorController(VendedorService service)
        {
            _service = service;
        }



        // GET: Vendedor
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllVendedoresAsync());
        }

        // GET: Vendedor/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var vendedor = await _service.GetVendedorByIdAsync(id);

            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataCadastro")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _service.CreateVendedorAsync(vendedor);
                if (resultado > 0)
                    return RedirectToAction(nameof(Index));

                return View(vendedor);
            }
            return View(vendedor);
        }

        // GET: Vendedor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var vendedor = await _service.GetVendedorByIdAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // POST: Vendedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataCadastro")] Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var rasultado = await _service.UpdateVendedorAsync(vendedor);
                
                if(rasultado > 0)
                    return RedirectToAction(nameof(Index));

                return View(vendedor);
            }
            return View(vendedor);
        }

        // GET: Vendedor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var vendedor = await _service.GetVendedorByIdAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // POST: Vendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedor = await _service.GetVendedorByIdAsync(id);
           
            if (vendedor != null)
            {
                var resultado = await _service.DeleteVendedorAsync(vendedor);

                if(resultado > 0)
                    return RedirectToAction(nameof(Index));

                return View(vendedor);
            }

            return View(vendedor);

        }

    }
}
