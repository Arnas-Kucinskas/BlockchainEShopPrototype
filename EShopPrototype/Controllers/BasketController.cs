using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopPrototype.Data;
using SharedItems.Models;
using EShopPrototype.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopPrototype.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly BasketRepository _basketRepository;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public BasketController(BasketRepository basketRepository, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _basketRepository = basketRepository;
            _jwtAuthenticationManager = jwtAuthenticationManager;
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
        [Authorize]
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromHeader] string Authorization, [FromBody] Basket product)
        {//Basket item
            //GetUserBasket()
            //Basket product = new Basket
            /*Basket product = new Basket()
            {
                ProductId = 1,
                ProductQuanity = 2
            };*/
            int userId = _jwtAuthenticationManager.GetClaim(Authorization);
            product.UserId = userId;

            
            /*Basket item = new Basket()
            {
                OrderNumer = 132,
                ProductId = 1,
                UserId = 1,
                ProductQuanity = 2
            };*/
            _basketRepository.AddorUpdateProductInBasket(product);
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
