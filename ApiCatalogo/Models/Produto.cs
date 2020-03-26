using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        
        [MaxLength(80)]
        public string Nome { get; set; }
       
        [MaxLength(300)]
        public string Descricao { get; set; }
       
        public decimal Preco { get; set; }
 
        [MaxLength(500)]
        public string ImagemUrl { get; set; }
        public float? Estoque { get; set; }
        public DateTime? DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
