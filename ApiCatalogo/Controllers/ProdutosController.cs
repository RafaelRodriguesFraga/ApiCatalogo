using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    /// <summary>
    /// Produtos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public ProdutosController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Listar Produtos
        /// </summary>
        /// <returns>Uma lista de produtos</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> ListarProdutos()
        {
            return _ctx.Produtos.AsNoTracking().ToList();
        }      

        /// <summary>
        /// Listar Produto por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Produto</returns>
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> ListarProdutoPorId(int id)
        {        

            Produto produto = _ctx.Produtos.Where(p => p.Id == id).AsNoTracking().FirstOrDefault();
            if (produto == null)
                return NotFound();

            return produto;
        }
       
        /// <summary>
        /// Cadastrar Produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CadastrarProduto([FromBody] Produto produto)
        {
            _ctx.Produtos.Add(produto);
            _ctx.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);
        }

        /// <summary>
        /// Alterar Produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("{id:int:min(1)}")]
        public ActionResult AlterarProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            _ctx.Entry(produto).State = EntityState.Modified;
            _ctx.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Excluir Produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<Produto> ExcluirProduto(int id)
        {
            Produto produto = _ctx.Produtos.Where(p => p.Id == id).FirstOrDefault();
            if (produto == null)
                return NotFound();

            _ctx.Produtos.Remove(produto);
            _ctx.SaveChanges();
            return produto;
        }
    }
}