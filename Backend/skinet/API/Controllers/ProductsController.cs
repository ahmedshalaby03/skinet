using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public StoreContext Context { get; }
        public ProductsController(StoreContext Context)
        {
            this.Context = Context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await Context.Products.ToListAsync();
            if (products is null) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var p = await Context.Products.FindAsync(id);
            if (p is null) return NotFound();
            return p;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
            return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id!= id || !ProductExist(id))
                return BadRequest("Cannot update product");

            Context.Entry(product).State = EntityState.Modified;
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var p = await Context.Products.FindAsync(id);
            if (p == null) return NotFound();
            Context.Products.Remove(p);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExist(int id)
        {
            return Context.Products.Any(p => p.Id == id);
        }
    }
}
