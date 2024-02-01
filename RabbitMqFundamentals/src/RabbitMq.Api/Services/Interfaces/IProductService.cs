using RabbitMq.Api.Models;

namespace RabbitMq.Api.Services.Interfaces
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProductAll();
        public Product GetProductById(int id);
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(int id);
    }
}
