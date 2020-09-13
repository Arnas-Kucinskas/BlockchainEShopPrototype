using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; } = "Test description";
        public virtual List<Basket> Baskets { get; }
        [NotMapped]
        public double Price { set; get; }
        [NotMapped]
        //public List<Tuple<DateTime, double>> PriceHistory;
        public List<PriceHistory> PriceHistoryList;
        // Price goes from blockchain
    }
}
