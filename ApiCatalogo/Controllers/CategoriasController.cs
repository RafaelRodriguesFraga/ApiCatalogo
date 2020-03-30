using System;
using System.Collections.Generic;
using System.Linq;
using ApiCatalogo.Context;
using ApiCatalogo.Models;
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
        private readonly AppDbContext _ctx;

        public CategoriasController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Listar Categoria
        /// </summary>
        /// <returns>Uma lista de Categorias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> ListarCategorias()
        {
            return _ctx.Categorias.AsNoTracking().ToList();
        }

        /// <summary>
        /// Listar Categoria por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> ListarCategoriaPorId(int id)
        {
            Categoria categoria = _ctx.Categorias.Where(p => p.Id == id).AsNoTracking().FirstOrDefault();
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
            _ctx.Categorias.Add(categoria);
            _ctx.SaveChanges();

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

            _ctx.Entry(categoria).State = EntityState.Modified;
            _ctx.SaveChanges();

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
            Categoria categoria = _ctx.Categorias.Where(p => p.Id == id).FirstOrDefault();
            if (categoria == null)
                return NotFound();

            _ctx.Categorias.Remove(categoria);
            _ctx.SaveChanges();
            return categoria;
        }
    }
}
