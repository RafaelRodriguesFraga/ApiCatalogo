using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.DTO
{
    public class CategoriaDTO
    {       
        public int Id { get; set; }
        public string Nome { get; set; }   
        public string ImagemUrl { get; set; }
        
        // Uma Categoria possui uma coleção de produtos
        // (vários produtos)
        public ICollection<ProdutoDTO> Produtos { get; set; }
    }
}
