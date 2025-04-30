using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LinkBuyApi.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;
        private readonly VendedorService _vendedorService;
        private readonly CategoriaService _categoriaService;

        public ProdutoController(ProdutoService service, VendedorService vendedorService, CategoriaService categoriaService)
        {
            _service = service;
            _vendedorService = vendedorService;
            _categoriaService = categoriaService;
        }

        [HttpGet("todos-produtos")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> GetAllProdutos()
        {
            var produto = await _service.GetAllProdutos();

            if (produto is null) return NotFound();

            return Ok(produto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var produto = await _service.GetDetalheProduto(id);

            if (produto is null) return NotFound();

            return Ok(produto);
        }


        [HttpPost("novo-produto")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] ProdutoInsert produto)
        {

            if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });

            var vendedor = await _vendedorService.GetVendedorByIdAsync(produto.VendedorId);

            if (vendedor == null) return NotFound("Vendedor não encontrado");

            var categoria = await _categoriaService.GetCategoriasByIdAsync(produto.CategoriaId);

            if (categoria == null) return NotFound("Categoria não encontrada");

            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(produto.ImagemUpload.FileName);

            await _service.CreateImage(produto.ImagemUpload, nomeArquivo);

            Produto inserirProduto = new Produto
            {
                CategoriaId = produto.CategoriaId,
                VendedorId = produto.VendedorId,
                Descricao = produto.Descricao,
                Estoque = produto.Estoque,
                Imagem = nomeArquivo,
                Valor = produto.Valor
            };

            var result = await _service.CreateProdutoAsync(inserirProduto);

            if (result > 0)
            {

                return CreatedAtAction("Get", new { id = inserirProduto.Id }, inserirProduto);
            }

            return BadRequest("ocorreu um erro ao criar o produto");
        }


        [HttpDelete("deletar/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var produto = await _service.GetDetalheProduto(id);

                if (produto == null)
                {

                    return NotFound("Produto não encontrado");
                }

                var result = await _service.DeleteProdutoAsync(produto);

                if (result > 0)
                {
                    await _service.DeleteImage(produto.Imagem);
                    return NoContent();
                }

                return BadRequest("Ocorreu um erro ao deletar o produto");
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao deletar o produto");
            }
        }
    }
}