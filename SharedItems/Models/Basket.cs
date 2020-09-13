using SharedItems.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedItems.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderNumer { get; set; }
        [Required]
        public User User { get; set; }
        public virtual int UserId { get; set; }
        [Required]
        public Product Product { get; set; }
        public virtual int ProductId { get; set; }
        [Required]
        public int ProductQuanity { get; set; } = 1;
        // product id
        // product quant
    }
}
