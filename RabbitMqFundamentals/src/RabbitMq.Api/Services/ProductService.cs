using RabbitMq.Api.Context;
using RabbitMq.Api.Models;
using RabbitMq.Api.Services.Interfaces;

namespace RabbitMq.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductAll()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
           return _context.Products.Find(id);
        }

        public Product AddProduct(Product product)
        {
            var result = _context.Products.Add(product);

            _context.SaveChanges();

            return result.Entity;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _context.Products.Update(product);

            _context.SaveChanges();

            return result.Entity;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);

            var result = _context.Remove(product);

            _context.SaveChanges();

            return result != null;
        }
    }
}
