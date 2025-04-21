using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkBuyMvc.Controllers
{
    public class AuthController : Controller
    {
        protected readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var result = await _authService.Login(login);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Categoria");
            }

            ModelState.AddModelError("", "Usuário ou senha inválidos");
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            var result = await _authService.Register(register);

            if (result > 0)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Erro ao registrar usuário");
            return View(register);
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}
