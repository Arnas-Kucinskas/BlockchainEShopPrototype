using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopPrototype.Data;
using EShopPrototype.Models;
using EShopPrototype.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopPrototype.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketRepository _basketRepository;

        public BasketController(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("GetUserBasket/{id}", Name = "GetUserBasket")]
        public async Task<IActionResult> GetUserBasket(int id)
        {
            var userBasket = await _basketRepository.GetMyBasket(id);
            return Ok(userBasket);
        }
        /* Make it generic patch request? */
        [HttpPost("ChangeQuanityt/{id}")]
        public IActionResult UpdateProductQuantity(int id, [FromBody]int productQuantity)
        {

            Basket basketProduct = _basketRepository.GetBasketById(id);
            if (basketProduct == null)
            {
                return NotFound();
            }
            basketProduct.ProductQuanity = productQuantity;
            _basketRepository.UpdateQuanityt(basketProduct);
            return Ok(basketProduct);
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct()
        {//Basket item
            //GetUserBasket()
            Basket item = new Basket()
            {
                OrderNumer = 132,
                ProductId = 1,
                UserId = 1,
                ProductQuanity = 2
            };
            _basketRepository.AddProductToBasket(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductFromBasket(int id)
        {
            Basket basketProduct = _basketRepository.GetBasketById(id);
            if (basketProduct == null)
            {
                return NotFound();
            }
            _basketRepository.DeleteBasketProduct(basketProduct);
            return NoContent();
            //_basketRepository.Get
            //_basketRepository.
        }
    }
}
