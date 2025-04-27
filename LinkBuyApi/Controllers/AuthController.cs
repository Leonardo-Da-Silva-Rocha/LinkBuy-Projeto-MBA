using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PrimeiraApi.Controllers
{
    [ApiController]
    [Route("api/conta")]
    public class AuthController : ControllerBase
    {
        protected readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar(RegisterViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _authService.Register(registerUser);

            if (result > 0)
            {
                return Ok(await _authService.GerarJwt(registerUser.Email));
            }

            return Problem("Falha ao registrar o usuário");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _authService.Login(loginUser);

            if (result.Succeeded)
            {
                return Ok(await _authService.GerarJwt(loginUser.Email));
            }

            return Problem("Usuario ou senha incorretos");
        }


    }
}
