using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Contexts;
using Training.DTOS;
using Training.Entities;

namespace Training.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Products")]
        [MapToApiVersion("1.0")]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsV1()
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

        [HttpGet("Products")]
        [MapToApiVersion("2.0")]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsV2()
        {
            var products = await context.Products.ToListAsync();
            var productDtos = products.Select(P => new ProductDto()
            {
                Id = P.Id,
                Name = P.Name,
                Price = P.Price,
                BrandName = P.ProductBrand.Name
            });

            return Ok(productDtos);
        }
    }
}
