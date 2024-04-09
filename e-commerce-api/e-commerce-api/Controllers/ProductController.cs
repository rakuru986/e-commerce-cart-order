using e_commerce_api.Models;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<Product> GetProducts()
        {
            var products = _productService.GetProducts();

            AddCustomHeaders(JsonConvert.SerializeObject(products));

            return Ok(products);
        }

        private void AddCustomHeaders(string contentLength)
        {

            int byteCount = Encoding.UTF8.GetByteCount(contentLength);

            Response.Headers.Add("Content-Length", byteCount.ToString());
            Response.Headers.Add("Connection", "keep-alive");
            Response.Headers.Add("Cache-Control", "max-age=0, private, must-revalidate");
            Response.Headers.Add("x-request-id", Guid.NewGuid().ToString());
        }
    }
}
