using Microsoft.AspNetCore.Mvc;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IProductService productService;

        public ListController(IProductService productService)
        {
            this.productService = productService;
        }
        public ActionResult Index()
        {
            productService.Test();
            return Ok();
        }
    }
}
