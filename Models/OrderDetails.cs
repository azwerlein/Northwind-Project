using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models;

public class OrderDetails
{
    [Required]
    public int OrderDetailsId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Column(TypeName="money")]
    public decimal UnitPrice { get; set; }
    [DefaultValue(1)]
    public short Quantity { get; set; }
    [Column(TypeName="decimal(5, 3)")]
    public decimal Discount { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}
