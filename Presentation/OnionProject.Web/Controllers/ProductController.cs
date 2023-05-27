using Microsoft.AspNetCore.Mvc;
using OnionProject.Application.Repositories;

namespace OnionProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { ProductId = 2 });
        }

        [HttpGet]
        [Route("Get2")]
        public IActionResult Get2()
        {
            return Ok(new { ProductId = 2 });
        } 

        [HttpGet("{id}")]
        public IActionResult Get3(string id)
        {
            return Ok(new { ProductId = 2 });
        }
    }
}
