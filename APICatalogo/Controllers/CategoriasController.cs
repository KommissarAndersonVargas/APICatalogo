﻿using APICatalogo.Context;
using APICatalogo.Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
           

            return _context.Categorias.ToList();

        }

        [HttpGet("{id:int}", Name = "Obter Categoria")]
        public ActionResult<Categoria> Get(int id)
        {

            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();
            
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("Obter categoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Categoria categoria)
        {

            if (id != categoria.CategoriaId)
            {
                return BadRequest("Categoria não encontrado");
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]

        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
            // procura o id por linq
            if (categoria is null)
            {
                return NotFound("Categoria não localizado");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria); // retorna o produto removido
        }
    }
}
