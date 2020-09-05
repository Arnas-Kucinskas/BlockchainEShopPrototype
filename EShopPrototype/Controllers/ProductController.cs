using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopPrototype.Data.Interfaces;
using EShopPrototype.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopPrototype.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
            return CreatedAtRoute(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult GetProductById(int id)
        {
            Product product = _productRepository.GetProductById(id);
            return Ok(product);
        }

    }
}
