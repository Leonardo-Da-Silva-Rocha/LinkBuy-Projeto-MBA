using System.ComponentModel.DataAnnotations;

namespace LinkBuyLibrary.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O produto precida ter uma descrição.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "O campo descrição precisa ter no minimo 5 caracteres e no maximo 50.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo valor é obrigatorio.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo de estoque é obrigatorio")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "A imagem do produto é obrigatoria")]
        public string? Imagem { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }


    }
}
