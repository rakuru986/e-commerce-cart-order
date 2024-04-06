namespace e_commerce_api.Models
{
    public class OrderProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product ReplacedWith { get; set; }
    }
}
