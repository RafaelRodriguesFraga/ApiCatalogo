using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(80)]
        public string Nome { get; set; }
                
        [MaxLength(300)]
        public string ImagemUrl { get; set; }

        // Uma Categoria possui uma coleção de produtos
        // (vários produtos)
        public ICollection<Produto> Produtos;

    }
}
