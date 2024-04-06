using e_commerce_api.Models;

namespace e_commerce_api.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
