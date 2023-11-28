using System;
using System.Collections.Generic;

namespace Northwind.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
