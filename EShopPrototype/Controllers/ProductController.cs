using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopPrototype.Data.Interfaces;
using SharedItems.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopPrototype.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
            return CreatedAtRoute(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(product);
            return NoContent();
        }

        /* Price update is not allowed   */
        [HttpPost("Update")]
        public  IActionResult UpdateProduct([FromBody] Product product)
        {
            _productRepository.UpdateProduct(product);
            return Ok();
            
        }

        [HttpGet("PageNumber/{pageNumber}/ItemsPerPage/{itemsPerPage}")]
        public async Task<IActionResult> GetPaginatedProducts(int pageNumber, int itemsPerPage)
        {
            List<Product> productList = await _productRepository.GetPaginatedProductsList(pageNumber, itemsPerPage);
            return Ok(productList);

        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            Product product = await _productRepository.GetProductById(id);
            return Ok(product);
        }

    }
}
