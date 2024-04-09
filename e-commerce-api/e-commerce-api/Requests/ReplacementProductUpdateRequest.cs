using e_commerce_api.Models;

namespace e_commerce_api.Requests
{
    public class ReplacementProductUpdateRequest
    {
        public ReplacedWithInfo ReplacedWith { get; set; }
    }

    public class ReplacedWithInfo
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
