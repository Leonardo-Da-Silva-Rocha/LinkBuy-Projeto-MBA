using System.ComponentModel.DataAnnotations;

namespace LinkBuyLibrary.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatorio.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
