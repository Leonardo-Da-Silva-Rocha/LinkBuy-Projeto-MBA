using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkBuyApi.Controllers
{
    [Authorize]
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet("todas-categorias")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> GetAllCategorias()
        {
            var categoria = await _service.GetAllCategoriasAsync();

            if (categoria is null) return NotFound();

            return Ok(categoria);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);

            if (categoria is null) return NotFound();

            return Ok(categoria);
        }

        [HttpPost("nova-categoria")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([Bind("Descricao")] Categoria categoria)
        {
            if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });

            var result = await _service.CreateCategoriaAsync(categoria);

            if (result > 0)
            {
                return CreatedAtAction("Get", new { id = categoria.Id }, categoria);
            }

            return BadRequest();
        }

        [HttpDelete("deletar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);
            try
            {
                int resultado = 0;

                if (categoria != null)
                {
                    resultado = await _service.DeleteCategoriaAsync(categoria);

                    if (resultado > 0) return NoContent();

                    else throw new Exception("ocorreu um erro ao deletar a categoria");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Não é possível excluir uma categoria com produtos associados.");
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro inesperado.");
            }

        }

        [HttpPut("editar/{id:int}")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [Bind("Id,Descricao")] Categoria categoria)
        {
            if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });

            var resultado = 0;

            if (id != categoria.Id)
            {
                return NotFound();
            }

            resultado = await _service.UpdateCategoriaAsync(categoria);

            if (resultado > 0)
            {
                return NoContent();
            }

            return BadRequest("Ocorreu um erro ao tentar editar a categoria");
        }

    }
}
