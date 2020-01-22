using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kroger.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string LocationID { get; set; }
        public string ProductName { get; set; }
        public float ProductRegularPrice { get; set; }
        public float ProductPromoPrice { get; set; }
        public DateTime CaptureDate { get; set; }
    }
}
