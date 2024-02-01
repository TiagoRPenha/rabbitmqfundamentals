using Microsoft.AspNetCore.Mvc;
using RabbitMq.Api.Models;
using RabbitMq.Api.RabbitMQ.Interfaces;
using RabbitMq.Api.Services.Interfaces;

namespace RabbitMq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public ProductsController(IProductService productService, IRabbitMQProducer rabbitMQProducer)
        {
            _productService = productService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            return _productService.GetProductAll();
        }

        [HttpGet("getproductbyid")]
        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost("addproduct")]
        public Product AddProduct(Product product)
        {
            var productData = _productService.AddProduct(product);

            _rabbitMQProducer.SendProductMessage(productData);

            return productData;
        }

        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product)
        { 
            var productData = _productService.UpdateProduct(product);

            _rabbitMQProducer.SendProductMessage(productData);

            return productData;
        }

        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int id)
        {
            return _productService.DeleteProduct(id);
        }
    }
}
