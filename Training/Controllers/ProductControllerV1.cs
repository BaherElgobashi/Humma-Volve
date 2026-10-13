using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Contexts;
using Training.DTOS;

namespace Training.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/Products")]
    public class ProductControllerV1 : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductControllerV1(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtoV1>>> GetAllProducts()
        {
            var products = await context.Products.ToListAsync();

            var productDtos = products.Select(p => new ProductDtoV1
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

            return Ok(productDtos);
        }
    }
}