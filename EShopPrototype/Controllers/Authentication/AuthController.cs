using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopPrototype.Data;
using EShopPrototype.Data.Interfaces;
using Shared.Models.Authentication;
using EShopPrototype.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopPrototype.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly PasswordManager _passwordManager;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthController(IAuthenticationRepository authenticationRepository, PasswordManager passwordManager, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _authenticationRepository = authenticationRepository;
            _passwordManager = passwordManager;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }
        
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User userCredentials)
        {
            string passwordHash = _passwordManager.GeneratePasswordHash(userCredentials.Password);
            userCredentials.Password = passwordHash;
            bool succeded = _authenticationRepository.Register(userCredentials);
            if (succeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] User userCredentials)
        {
            User user =_authenticationRepository.GetUserByName(userCredentials.Username);
            if (user == null)
            {
                return BadRequest();
            }

            var token = _jwtAuthenticationManager.Authenticate(userCredentials.Username, userCredentials.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
