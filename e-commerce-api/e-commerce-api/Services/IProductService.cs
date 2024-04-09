using e_commerce_api.Models;

namespace e_commerce_api.Services
{
    public interface IProductService
    {
        Product GetProductById(int id);
        List<Product> GetProducts();
        List<Product> GetProductsByIds(List<int> productIds);

    }
}
