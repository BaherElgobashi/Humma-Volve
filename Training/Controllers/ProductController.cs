using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Contexts;
using Training.DTOS;
using Training.Entities;

namespace Training.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Products")]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await context.Products.ToListAsync();
            var productDtos = products.Select(P => new ProductDto() 
            {
                Id = P.Id,
                Name = P.Name,
                Price = P.Price
            });

            return Ok(productDtos);
        }
    }
}
