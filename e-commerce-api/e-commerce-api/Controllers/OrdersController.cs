using e_commerce_api.Models;
using e_commerce_api.Requests;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace e_commerce_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder()
        {
            var newOrder = _orderService.CreateOrder();

            AddCustomHeaders(JsonConvert.SerializeObject(newOrder));

            return StatusCode(201, newOrder);
        }

        [HttpGet("{order_id}")]
        public ActionResult<Order> GetOrder(string order_id)
        {
            try
            {
                var order = _orderService.GetOrder(order_id);

                AddCustomHeaders(JsonConvert.SerializeObject(order));

                return Ok(order);
            }
            catch (Exception ex)
            {
                AddCustomHeaders(JsonConvert.SerializeObject(ex.Message));
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            } 
        }

        [HttpPatch("{order_id}")]
        public IActionResult UpdateOrder(string order_id, [FromBody] OrderUpdateRequest request)
        {
            try
            {
                _orderService.UpdateOrder(order_id, request);
                
                var response = JsonConvert.SerializeObject("OK");
                //AddCustomHeaders(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                AddCustomHeaders(JsonConvert.SerializeObject(ex.Message));
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
        }

        [HttpGet("{order_id}/products")]
        public ActionResult<OrderProduct> GetOrderProducts(string order_id)
        {
            try
            {
                var products = _orderService.GetOrderProducts(order_id);

                AddCustomHeaders(JsonConvert.SerializeObject(products));

                return Ok(products);
            }
            catch (Exception ex)
            {
                AddCustomHeaders(JsonConvert.SerializeObject(ex.Message));
                return NotFound(JsonConvert.SerializeObject(ex.Message));
            }
            
        }

        [HttpPost("{order_id}/products")]
        public IActionResult AddProductsToOrder(string order_id, [FromBody] List<int>? productIds)
        {
            try
            {
                _orderService.AddProductsToOrder(order_id, productIds);

                var response = JsonConvert.SerializeObject("OK");
                //AddCustomHeaders(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                AddCustomHeaders(JsonConvert.SerializeObject(ex.Message));
                return NotFound(JsonConvert.SerializeObject(ex.Message));
            }
            
        }

        [HttpPatch("{order_id}/products/{product_id}")] 
        public IActionResult UpdateProductQuantity(string order_id, string product_id, [FromBody] ProductQuantityUpdateRequest request)
        {
            try
            {
                _orderService.UpdateProductQuantity(order_id, product_id, request);

                var response = JsonConvert.SerializeObject("OK");
                // AddCustomHeaders(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                AddCustomHeaders(JsonConvert.SerializeObject(ex.Message));
                return NotFound(JsonConvert.SerializeObject(ex.Message));
            }
        }

        [HttpPost("{order_id}/products/{product_id}")]
        public IActionResult ReplaceProduct(string order_id, string product_id, [FromBody] ReplacementProductUpdateRequest request)
        {
            try
            {
                _orderService.ReplaceProduct(order_id, product_id, request);
                var response = JsonConvert.SerializeObject("OK");
                // AddCustomHeaders(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                AddCustomHeaders(JsonConvert.SerializeObject(ex.Message));
                return NotFound(JsonConvert.SerializeObject(ex.Message));
            }
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
