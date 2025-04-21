using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LinkBuyMvc.Models;
using LinkBuyLibrary.Services;

namespace LinkBuyMvc.Controllers;

public class HomeController : Controller
{
    protected readonly ProdutoService _produtoService;

    public HomeController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoService.GetAllProdutos();
        return View(produtos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
