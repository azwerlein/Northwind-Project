using System.ComponentModel.DataAnnotations;

namespace Northwind.Models;

public class Review
{
    [Required]
    public int CustomerID { get; set; }
    public Customer Customer { get; set; }
    
    [Required]
    public int ProductID { get; set; }
    public Product Product { get; set; }
    
    [Required]
    public int Rating { get; set; }
    
    [Required]
    public string ReviewText { get; set; }
}