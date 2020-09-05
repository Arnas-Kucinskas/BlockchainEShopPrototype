using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype.Models.Authentication
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        [Required]
        public string Username { get; set; }
        [MaxLength(256)]
        [MinLength(6)]
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public virtual List<Basket> Baskets { get; }
    }
}
