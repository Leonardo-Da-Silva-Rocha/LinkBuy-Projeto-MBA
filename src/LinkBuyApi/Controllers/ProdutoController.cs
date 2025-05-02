using LinkBuyLibrary.Models;
using LinkBuyLibrary.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("buscar-produto/{CategoriaId:int}")]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> GetProdutoByCategoria(int CategoriaId)
        {
            var produto = await _service.GetProdutoByCategoria(CategoriaId);

            if (produto is null || produto.Count() <= 0) return NotFound("Nenhum produto encontrado com a categoria infomada");

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


        [HttpPut("editar-produto/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put(int id, [FromForm] ProdutoInsert produtoInsert)
        {

            if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState)
            {
                Title = "Um ou mais erros de validação ocorreram!"
            });

            var vendedor = await _vendedorService.GetVendedorByIdAsync(produtoInsert.VendedorId);

            if (vendedor == null) return NotFound("Vendedor não encontrado");

            var categoria = await _categoriaService.GetCategoriasByIdAsync(produtoInsert.CategoriaId);

            if (categoria == null) return NotFound("Categoria não encontrada");

            var produtoEdit = await _service.GetDetalheProduto(id);

            if (produtoEdit == null) return NotFound("Produto não encontrado");

            await _service.DeleteImage(produtoEdit.Imagem);

            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(produtoInsert.ImagemUpload.FileName);

            await _service.CreateImage(produtoInsert.ImagemUpload, nomeArquivo);

            var produto = new Produto()
            {
                Id = id,
                Descricao = produtoInsert.Descricao,
                Estoque = produtoInsert.Estoque,
                Valor = produtoInsert.Valor,
                Imagem = nomeArquivo,
                VendedorId = produtoInsert.VendedorId,
                CategoriaId = produtoInsert.CategoriaId,
            };

            var result = await _service.EditProdutoAsync(produto);

            if (result > 0)
            {
                return NoContent();
            }

            return BadRequest("Ocorreu um erro ao tentar editar o produto");
        }
    }
}