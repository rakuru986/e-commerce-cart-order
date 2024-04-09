using e_commerce_api.Models;
using e_commerce_api.Requests;
using e_commerce_api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            return CreatedAtAction(nameof(GetOrder), new { order_id = newOrder.Id }, newOrder);
        }

        [HttpGet("{order_id}")]
        public ActionResult<Order> GetOrder(string order_id)
        {
            var order = _orderService.GetOrder(order_id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPatch("{order_id}")]
        public IActionResult UpdateOrder(string order_id, [FromBody] OrderUpdateRequest request)
        {
            _orderService.UpdateOrder(order_id, request);
            return Ok("OK");
        }

        [HttpGet("{order_id}/products")]
        public ActionResult<OrderProduct> GetOrderProducts(string order_id)
        {
            var products = _orderService.GetOrderProducts(order_id);
            return Ok(products);
        }

        [HttpPost("{order_id}/products")]
        public IActionResult AddProductsToOrder(string order_id, [FromBody] List<int>? productIds)
        {
            try
            {
                _orderService.AddProductsToOrder(order_id, productIds);
                return CreatedAtAction(nameof(GetOrderProducts), new { order_id }, null);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex.Message));
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    errors = new
                    {
                        detail = "Bad Request"
                    }
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpPatch("{order_id}/products/{product_id}")]
        public IActionResult UpdateProductQuantity(string order_id, string product_id, [FromBody] ProductQuantityUpdateRequest request)
        {
            _orderService.UpdateProductQuantity(order_id, product_id, request);
            return NoContent() ;
        }

        [HttpPatch("{order_id}/products/{product_id}")]
        public IActionResult ReplaceProduct(string order_id, string product_id, [FromBody] ReplacementProductUpdateRequest request)
        {
            try
            {
                _orderService.ReplaceProduct(order_id, product_id, request);
                return Ok("Product replaced successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = new { detail = "Bad Request" } });
            }
        }
    }
}
