﻿using ApiCatalogo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.DTO
{
    public class ProdutoDTO
    {
       
        public int Id { get; set; }       
        public string Nome { get; set; }      
        public string Descricao { get; set; }
        public decimal Preco { get; set; }        
        public string ImagemUrl { get; set; }
        public float? Estoque { get; set; }
        public DateTime? DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}
