namespace e_commerce_api.Models
{
    public class Order
    {
        public Amount Amount { get; set; }
        public string Id { get; set; }
        public List<OrderProduct> Products { get; set; }
        public string Status { get; set; }
    }
}
