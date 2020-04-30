using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
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
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Listar Produtos
        /// </summary>
        /// <returns>Uma lista de produtos</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> ListarProdutos()
        {
            return _unitOfWork.ProdutoRepository.Get().ToList();
        }

        /// <summary>
        /// Lista Produtos por Preço
        /// </summary>
        /// <returns>Produtos pelo seu preço</returns>
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _unitOfWork.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        /// <summary>
        /// Listar Produto por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Produto</returns>  
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> ListarProdutoPorId(int id)
        {

            Produto produto = _unitOfWork.ProdutoRepository.GetById(p => p.Id ==  id);

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
            _unitOfWork.ProdutoRepository.Add(produto);
            _unitOfWork.Commit();

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

            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();

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
            Produto produto = _unitOfWork.ProdutoRepository.GetById(p => p.Id == id);
            if (produto == null)
                return NotFound();

            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            return produto;
        }
    }
}