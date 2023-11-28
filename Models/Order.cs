using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models;

public class Order
{
    public Order()
    {
        OrderDetails = new HashSet<OrderDetails>();
    }

    [Required]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public int ShipVia { get; set; }
    [Column(TypeName="money")]
    [DefaultValue(0)]
    public decimal Freight { get; set; }
    [StringLength(40)]
    public string ShipName { get; set; }
    [StringLength(60)]
    public string ShipAddress { get; set; }
    [StringLength(15)]
    public string ShipCity { get; set; }
    [StringLength(15)]
    public string ShipRegion { get; set; }
    [StringLength(10)]
    public string ShipPostalCode { get; set; }
    [StringLength(15)]
    public string ShipCountry { get; set; }

    public Customer Customer { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
}
