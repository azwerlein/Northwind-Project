using System;
using System.Collections.Generic;

namespace Northwind.Models
{
    public partial class Review
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
