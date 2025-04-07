
using System.ComponentModel.DataAnnotations;

namespace LinkBuyLibrary.Models
{
    public class Vendedor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "O nome precisa ter no minimo 5 caracteres e no maximo 50.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo data de cadastro é obrigatorio")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; }

        public IEnumerable <Produto>? Produtos { get; set; }
    }
}
