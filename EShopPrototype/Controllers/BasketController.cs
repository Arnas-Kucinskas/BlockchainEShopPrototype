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
        public IActionResult GetUserBasket(int id)
        {
            var userBasket = _basketRepository.GetMyBasket(id);
            return Ok();
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
                //User = new User() {   },
                //Product = new Product() {  },
                ProductQuanity = 2
            };
            _basketRepository.AddProductToBasket(item);
            return Ok();
        }

        public IActionResult GetBasketById(int id)
        {
            //Product product = _basketRepository.G(id);
            //return Ok(product);
            return null;
        }

    }
}
