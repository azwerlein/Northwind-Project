using Microsoft.EntityFrameworkCore;

namespace Northwind.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

  public DbSet<Product> Products { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Discount> Discounts { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderDetails> OrderDetails { get; set; }

  public DbSet<Review> Reviews { get; set; }

  public DbSet<CartItem> CartItems { get; set; }

  public void AddCustomer(Customer customer)
  {
    Customers.Add(customer);
    SaveChanges();
  }
  public void EditCustomer(Customer customer)
  {
    var customerToUpdate = Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
    customerToUpdate.Address = customer.Address;
    customerToUpdate.City = customer.City;
    customerToUpdate.Region = customer.Region;
    customerToUpdate.PostalCode = customer.PostalCode;
    customerToUpdate.Country = customer.Country;
    customerToUpdate.Phone = customer.Phone;
    customerToUpdate.Fax = customer.Fax;
    SaveChanges();
  }

  public CartItem AddToCart(CartItemJSON cartItemJSON)
    {
        int CustomerId = Customers.FirstOrDefault(c => c.Email == cartItemJSON.Email)?.CustomerId ?? -1;
        int ProductId = cartItemJSON.ID;
        // check for duplicate cart item
        CartItem cartItem = CartItems.FirstOrDefault(ci => ci.ProductId == ProductId && ci.CustomerId == CustomerId);
        if (cartItem == null)
        {
            // this is a new cart item
            cartItem = new CartItem()
            {
                CustomerId = CustomerId,
                ProductId = cartItemJSON.ID,
                Quantity = cartItemJSON.Qty
            };
            CartItems.Add(cartItem);
        }
        else
        {
            // for duplicate cart item, simply update the quantity
            cartItem.Quantity += cartItemJSON.Qty;
        }

        SaveChanges();
        cartItem.Product = Products.Find(cartItem.ProductId);
        return cartItem;
    }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Review>()
      .HasKey(instance => new { instance.ProductID, instance.CustomerID });
    modelBuilder.Entity<OrderDetails>()
      .HasKey(e => e.OrderDetailsId);
  }
}