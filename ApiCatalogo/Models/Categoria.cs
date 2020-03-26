using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public int Id { get; set; }
        public int Nome { get; set; }
        public int ImagemUrl { get; set; }

        // Uma Categoria possui uma coleção de produtos
        // (vários produtos)
        public ICollection<Produto> Produtos;

    }
}
