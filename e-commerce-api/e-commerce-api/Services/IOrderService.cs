using e_commerce_api.Models;
using e_commerce_api.Requests;

namespace e_commerce_api.Services
{
    public interface IOrderService
    {
        Order CreateOrder();
        Order GetOrder(string order_id);
        void UpdateOrder(string order_id, OrderUpdateRequest request);
        List<OrderProduct> GetOrderProducts(string order_id);
        Order AddProductsToOrder(string order_id, List<int> productIds);
        Order UpdateProductQuantity(string order_id, string product_id, ProductQuantityUpdateRequest request);
        Order ReplaceProduct(string order_id, string product_id, ReplacementProductUpdateRequest request);
    }
}
