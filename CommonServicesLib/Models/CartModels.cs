using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib.Models
{
    public class CartItem
    {
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public string BookTitle { get; set; }
    }

    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
