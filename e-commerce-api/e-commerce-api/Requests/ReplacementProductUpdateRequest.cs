using e_commerce_api.Models;

namespace e_commerce_api.Requests
{
    public class ReplacementProductUpdateRequest
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public OrderProduct ReplacedWith { get; set; }
    }
}
