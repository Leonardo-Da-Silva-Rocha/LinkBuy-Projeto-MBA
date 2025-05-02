using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LinkBuyLibrary.Models
{
    public class ProdutoInsert
    {
        
        [Required(ErrorMessage = "O produto precida ter uma descrição.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "O campo descrição precisa ter no minimo 5 caracteres e no maximo 50.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Informe um valor válido. EX: 000.00")]

        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo de estoque é obrigatorio")]
        [Range(1, 9999, ErrorMessage = "O campo deve contter um valor entre 1 e 9999")]
        public int Estoque { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "A imagem do produto é obrigatória")]
        public IFormFile? ImagemUpload { get; set; }


        [Required(ErrorMessage = "A categoria é obrigatória")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O vendedor é obrigatorio")]
        public int VendedorId { get; set; }

        
    }
}
