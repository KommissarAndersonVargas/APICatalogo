using APICatalogo.Context;
using APICatalogo.Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("a[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() 
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
           

                return produtos;
            
        }
        [HttpGet("{id:int}", Name ="Obter Produto")]
        public ActionResult<Produto> Get(int id)
        {

            var produto  = _context.Produtos.FirstOrDefault(p=> p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Id de produto não encontrado");
            }

            return produto;
        }
        [HttpPost]
        public ActionResult Post (Produto produto)
        {
            if(produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("Obter Produto",
                new { id = produto.ProdutoId }, produto);
        }


        [HttpPut("{id:int}")]

        public ActionResult Put (int id, Produto produto)
        {

            if(id != produto.ProdutoId)
            {
                return BadRequest("Produto não encontrado");
            }
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        [HttpDelete("{id:int}")]

        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            // procura o id por linq
            if(produto is null)
            {
                return NotFound("Produto não localizado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto); // retorna o produto removido
        }

    }
}
