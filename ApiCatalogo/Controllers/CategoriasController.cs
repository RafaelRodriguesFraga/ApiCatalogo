using System;
using System.Collections.Generic;
using System.Linq;
using ApiCatalogo.Context;
using ApiCatalogo.DTO;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Listar Categoria
        /// </summary>
        /// <returns>Uma lista de Categorias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> ListarCategorias()
        {
            List<Categoria> categorias = _unitOfWork.CategoriaRepository.Get().ToList();
            List<CategoriaDTO> categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

            return categoriasDto;

        }

        /// <summary>
        /// Listar Produtos de uma Categoria
        /// </summary>
        /// <returns></returns>
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> ListarCategoriasProdutos()
        {
            List<Categoria> categorias =  _unitOfWork.CategoriaRepository.GetCategoriasProdutos().ToList();
            List<CategoriaDTO> categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

            return categoriasDto;
        }
        
        /// <summary>
        /// Listar Categoria por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> ListarCategoriaPorId(int id)
        {
            Categoria categoria = _unitOfWork.CategoriaRepository.GetById(c => c.Id == id);
            if (categoria == null)
                return NotFound();

            CategoriaDTO categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }

        /// <summary>
        /// Cadastrar Categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CadastrarCategoria([FromBody] CategoriaDTO categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            _unitOfWork.CategoriaRepository.Add(categoria);
            _unitOfWork.Commit();


            CategoriaDTO categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoriaDTO);
        }

        /// <summary>
        /// Alterar Categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult AlterarCategoria(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.Id)
                return BadRequest();

            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);

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
        public ActionResult<CategoriaDTO> ExcluirCategoria(int id)
        {
            Categoria categoria = _unitOfWork.CategoriaRepository.GetById(c => c.Id == id);
            if (categoria == null)
                return NotFound();

            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();

            CategoriaDTO categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;
        }
    }
}
