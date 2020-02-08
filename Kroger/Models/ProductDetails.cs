using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kroger.Models
{
    public class ProductDetails
    {
        public string ProductId { get; set; }
        public string LocationId { get; set; }
        public string ProductName { get; set; }
        public float PriceToday { get; set; }
        public float MaxPrice { get; set; }
        public float AveragePrice { get; set; }
        public float MinPrice { get; set; }
        public float TimeOnClearance { get; set; }
    }
}
