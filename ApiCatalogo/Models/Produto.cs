using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Models
{
    public class Produto
    {        
        public int Id { get; set; }        
        [MaxLength(80)]
        public string Nome { get; set; }
       
        [MaxLength(300)]
        public string Descricao { get; set; }
       
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1,1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }
 
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float? Estoque { get; set; }
        public DateTime? DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
