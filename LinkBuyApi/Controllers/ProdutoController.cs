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
        private readonly CategoriaService _serviceCategoria;
        private readonly VendedorService _vendedorService;

        public ProdutoController(ProdutoService service, CategoriaService serviceCategoria, VendedorService vendedorService)
        {
            _service = service;
            _serviceCategoria = serviceCategoria;
            _vendedorService = vendedorService;
        }


        [HttpPost("novo-produto")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([Bind("Id,Descricao,Valor,Estoque,ImagemUpload,CategoriaId")] Produto produto)
        {

            if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var vendedor = await _vendedorService.GetVendedorByIdLoginAsync(userIdString);

            if (vendedor == null) return NotFound();

            produto.Vendedor = vendedor;

            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(produto.ImagemUpload.FileName);

            await _service.CreateImage(produto.ImagemUpload, nomeArquivo);

            produto.Imagem = nomeArquivo;

            var result = await _service.CreateProdutoAsync(produto);

            if (result > 0)
            {

                return Created();
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