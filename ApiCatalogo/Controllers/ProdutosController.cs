using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.Context;
using ApiCatalogo.DTO;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Listar Produtos
        /// </summary>
        /// <returns>Uma lista de produtos</returns>
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<ProdutoDTO>> ListarProdutos()
        {
            List<Produto> produtos =  _unitOfWork.ProdutoRepository.Get().ToList();
            List<ProdutoDTO> produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDTO;
        }

        /// <summary>
        /// Lista Produtos por Preço
        /// </summary>
        /// <returns>Produtos pelo seu preço</returns>
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {
            List<Produto> produtos =  _unitOfWork.ProdutoRepository.GetProdutosPorPreco().ToList();
            List<ProdutoDTO> produtoDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtoDTO;
        }

        /// <summary>
        /// Listar Produto por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Produto</returns>  
        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> ListarProdutoPorId(int id)
        {

            Produto produto = _unitOfWork.ProdutoRepository.GetById(p => p.Id ==  id);

            if (produto == null)
                return NotFound();

            ProdutoDTO produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return produtoDTO;

         
        }
       
        /// <summary>
        /// Cadastrar Produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CadastrarProduto([FromBody] ProdutoDTO produtoDto)
        {
            Produto produto = _mapper.Map<Produto>(produtoDto);

            _unitOfWork.ProdutoRepository.Add(produto);
            _unitOfWork.Commit();

            ProdutoDTO produtoDTO = _mapper.Map<ProdutoDTO>(produto); 

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produtoDTO);
        }

        /// <summary>
        /// Alterar Produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("{id:int:min(1)}")]
        public ActionResult AlterarProduto(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (id != produtoDto.Id)
                return BadRequest();

            Produto produto = _mapper.Map<Produto>(produtoDto);

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
        public ActionResult<ProdutoDTO> ExcluirProduto(int id)
        {
            Produto produto = _unitOfWork.ProdutoRepository.GetById(p => p.Id == id);
            if (produto == null)
                return NotFound();

            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();

            ProdutoDTO produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return produtoDto;
        }
    }
}