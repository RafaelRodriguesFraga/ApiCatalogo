using System;
using System.Collections.Generic;
using System.Linq;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers
{
    /// <summary>
    /// Categorias
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Listar Categoria
        /// </summary>
        /// <returns>Uma lista de Categorias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> ListarCategorias()
        {
            return _unitOfWork.CategoriaRepository.Get().ToList();
        }

        /// <summary>
        /// Listar Produtos de uma Categoria
        /// </summary>
        /// <returns></returns>
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> ListarCategoriasProdutos()
        {
            return _unitOfWork.CategoriaRepository.GetCategoriasProdutos().ToList();
        }
        
        /// <summary>
        /// Listar Categoria por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> ListarCategoriaPorId(int id)
        {
            Categoria categoria = _unitOfWork.CategoriaRepository.GetById(c => c.Id == id);
            if (categoria == null)
                return NotFound();

            return categoria;
        }

        /// <summary>
        /// Cadastrar Categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CadastrarCategoria([FromBody] Categoria categoria)
        {
            _unitOfWork.CategoriaRepository.Add(categoria);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);
        }

        /// <summary>
        /// Alterar Categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult AlterarCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();

            return Ok();
        }

        /// <summary>
        /// Excluir Categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<Categoria> ExcluirCategoria(int id)
        {
            Categoria categoria = _unitOfWork.CategoriaRepository.GetById(c => c.Id == id);
            if (categoria == null)
                return NotFound();

            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();
            return categoria;
        }
    }
}
