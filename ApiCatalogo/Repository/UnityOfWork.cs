﻿using ApiCatalogo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        private ProdutoRepository _produtoRepo;
        private CategoriaRepository _categoriaRepo;
        public AppDbContext _context;

        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository => _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);

        public ICategoriaRepository CategoriaRepository => _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
