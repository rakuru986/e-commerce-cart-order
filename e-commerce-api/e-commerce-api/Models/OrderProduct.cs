namespace e_commerce_api.Models
{
    public class OrderProduct
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Price { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public Product ReplacedWith { get; set; }
    }
}
