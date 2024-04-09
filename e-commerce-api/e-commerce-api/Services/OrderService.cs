using e_commerce_api.Models;
using e_commerce_api.Requests;
using Microsoft.Extensions.Primitives;
using System.Globalization;

namespace e_commerce_api.Services
{
    public class OrderService : IOrderService
    {
        private static readonly List<Order> _orders = new List<Order>();
        private readonly IProductService _productService;

        public OrderService(IProductService productService)
        { 
            _productService = productService;
        }

        public Order CreateOrder()
        {
            string orderId = Guid.NewGuid().ToString();

            var newOrder = new Order
            {
                Id = orderId,
                Status = "NEW",
                Products = new List<OrderProduct>(),
                Amount = new Amount
                {
                    Discount = "0.00",
                    Paid = "0.00",
                    Returns = "0.00",
                    Total = "0.00"
                }
            };

            _orders.Add(newOrder);
            return newOrder;
        }

        public Order GetOrder(string order_id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == order_id);
            
            if (order == null)
            {
                throw new Exception("Not found");
            }

            var responseOrder = new Order
            {
                Id = order.Id,
                Status = order.Status,
                Amount = order.Amount,
                Products = order.Products.Select(op => new OrderProduct
                {
                    Id = op.Id,
                    Name = op.Name,
                    Price = op.Price,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    ReplacedWith = op.ReplacedWith
                }).ToList()
            };

            return responseOrder;
        }

        public void UpdateOrder(string order_id, OrderUpdateRequest request)
        {
            var order = _orders.FirstOrDefault(o => o.Id == order_id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                if (request.Status.ToUpper() != "PAID")
                {
                    throw new Exception("Invalid order status");
                }

                order.Status = request.Status;
            }
        }

        public List<OrderProduct> GetOrderProducts(string order_id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == order_id);

            if (order == null)
            {
                throw new Exception("Not found");
            }

            return order.Products;
        }

        public Order AddProductsToOrder(string order_id, List<int> productIds)
        {
            var order = _orders.FirstOrDefault(o => o.Id == order_id);
            var products = _productService.GetProductsByIds(productIds);

            if (order == null)
            {
                throw new Exception("Not found");
            }

            if (products.Count == 0)
            {
                throw new Exception("Invalid parameters");
            }


            foreach (var product in products)
            {
                var existingOrderProduct = order.Products.FirstOrDefault(op => op.ProductId == product.Id);
                if (existingOrderProduct != null)
                {
                    existingOrderProduct.Quantity++;
                }
                else
                {
                    order.Products.Add(new OrderProduct
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = 1
                    });
                }

                decimal total;
                decimal.TryParse(order.Amount.Total, NumberStyles.Number, CultureInfo.InvariantCulture, out total);

                decimal price;
                decimal.TryParse(product.Price, NumberStyles.Number, CultureInfo.InvariantCulture, out price);

                decimal newTotal = total + price;

                order.Amount.Total = newTotal.ToString();
            }

            return order;
        }

        public Order UpdateProductQuantity(string order_id, string product_id, ProductQuantityUpdateRequest request)
        {
            var order = _orders.FirstOrDefault(o => o.Id == order_id);

            if (order == null)
            {
                throw new Exception("Not found");
            }

            var orderProduct = order.Products.FirstOrDefault(op => op.Id == product_id);

            if (orderProduct == null)
            {
                throw new Exception("Not found");
            }

            if (request.Quantity.HasValue)
            {
                orderProduct.Quantity = request.Quantity.Value;
            }

            order.Amount.Total = order.Products.Sum(op => decimal.Parse(op.Price, CultureInfo.InvariantCulture) * op.Quantity).ToString();

            return order;
        }

        public Order ReplaceProduct(string order_id, string product_id, ReplacementProductUpdateRequest request)
        {
            var order = _orders.FirstOrDefault(o => o.Id == order_id);

            if (order == null)
            {
                throw new Exception("Not found");
            }

            var orderProduct = order.Products.FirstOrDefault(op => op.Id == product_id);

            if (orderProduct == null)
            {
                throw new Exception("Not found");
            }

            if (request.ReplacedWith != null && request.ReplacedWith?.Quantity != null)
            {
                var newProduct = _productService.GetProductById(request.ReplacedWith.Id);

                if (newProduct != null)
                {
                    orderProduct.ReplacedWith = newProduct;
                    orderProduct.Quantity = request.ReplacedWith.Quantity; 

                    order.Amount.Total = order.Products.Sum(op => decimal.Parse(newProduct.Price, CultureInfo.InvariantCulture) * op.Quantity).ToString();
                }
                else
                {
                    throw new Exception("Not found");
                }
            }
            return order;
        }
    }
}
