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
        public async Task<ActionResult<IEnumerable<ProductDtoV2>>> GetAllProducts([FromQuery] int pageIndex =1 , [FromQuery] int pageSize =10 )
        {
            if (pageIndex < 1)
                pageIndex = 1;

            if (pageSize < 1 || pageSize >50)
                pageSize = 10;


            var products = await context.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(p => new ProductDtoV2
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    BrandName = p.ProductBrand.Name
                }).ToListAsync() ;
               

            

            return Ok(products);
        }
    }
}