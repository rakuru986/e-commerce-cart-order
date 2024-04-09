using e_commerce_api.Models;

namespace e_commerce_api.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { Id = 123, Name = "Ketchup", Price = "0.45" },
                new Product { Id = 456, Name = "Beer", Price = "2.33" },
                new Product { Id = 879, Name = "Õllesnäkk", Price = "0.42" },
                new Product { Id = 999, Name = "75\" OLED TV", Price = "1333.37" }
            };
        }

        public Product GetProductById(int productId)
        {
            var selectedProduct = _products.FirstOrDefault(p => p.Id == productId);
            return selectedProduct;
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public List<Product> GetProductsByIds(List<int> productIds)
        {
            var selectedProducts = _products.Where(p => productIds.Contains(p.Id)).ToList();

            return selectedProducts;
        }
    }
}
