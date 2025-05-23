﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LinkBuyLibrary.Models
{
    public class Vendedor
    {
        [Key]
        public int Id { get; set; }

        public string FkLogin { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "O nome precisa ter no minimo 5 caracteres e no maximo 50.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo data de cadastro é obrigatorio")]
        public DateTime DataCadastro { get; set; }

        [JsonIgnore]
        public IEnumerable <Produto>? Produtos { get; set; }
    }
}
