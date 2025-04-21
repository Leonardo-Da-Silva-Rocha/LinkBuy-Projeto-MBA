using System.ComponentModel.DataAnnotations;

namespace LinkBuyLibrary.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O email é obrigatorio.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Entre com um email valido")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "O campo nome é obrigatorio.")]
        public string? Nome { get; set; }


        [Required(ErrorMessage = "A senha é obrigatoria.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirme sua senha.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas precisam ser identicas")]
        public string? ConfirmPassword { get; set; }
    }
}