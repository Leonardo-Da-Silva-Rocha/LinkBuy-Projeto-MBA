﻿
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LinkBuyLibrary.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "A descrição é obrigatoria.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "O campo descrição precisa ter no minimo 5 caracteres e no maximo 50.")]
        public string? Descricao { get; set; }

        [JsonIgnore]
        public IEnumerable<Produto>? Produtos { get; set; }
    }
}