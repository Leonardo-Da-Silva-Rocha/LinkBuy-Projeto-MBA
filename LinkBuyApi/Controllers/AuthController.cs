using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Mvc;


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

        [HttpPost("criar-conta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registrar(RegisterViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            if(await _authService.CheckUserExists(registerUser.Email)) return BadRequest("Usuario já cadastrado com este email.");

            var result = await _authService.Register(registerUser);

            if (result > 0)
            {
                return Ok(await _authService.GerarJwt(registerUser.Email));
            }

            return Problem("Falha ao registrar o usuário, verifique se o email esta correto e se a senha tem no minimo 5 caracteres");
        }

        [HttpPost("autenticar")]
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
