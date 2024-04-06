using e_commerce_api.Models;

namespace e_commerce_api.Services
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 123, Name = "Ketchup", Price = 0.45m },
                new Product { Id = 456, Name = "Beer", Price = 2.33m },
                new Product { Id = 879, Name = "Õllesnäkk", Price = 0.42m },
                new Product { Id = 999, Name = "75\" OLED TV", Price = 1333.37m }
            };

            return products;
        }
    }
}
