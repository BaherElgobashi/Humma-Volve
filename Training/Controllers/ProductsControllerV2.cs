using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training.Contexts;
using Training.DTOS;

namespace Training.Controllers
{
    [ApiController]
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/Products")]
    public class ProductsControllerV2 : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsControllerV2(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDtoV1>>> GetAllProducts()
        {
            var products = await context.Products
                .Include(p => p.ProductBrand)
                .ToListAsync();

            var productDtos = products.Select(p => new ProductDtoV1
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                BrandName = p.ProductBrand.Name
            });

            return Ok(productDtos);
        }
    }
}