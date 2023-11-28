using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Controllers.api;

[Route("api/customer")]
public class CustomerApiController : Controller
{
    private readonly DataContext _dataContext;
    public CustomerApiController(DataContext db)
    {
        _dataContext = db;
    }

    [Authorize]
    [HttpGet]
    public string ID() => User.Identity.GetUserId();

    [Authorize]
    [HttpGet]
    public string Email() => User.Identity?.Name ?? "Missing Email";


    [HttpGet, Route("{id:int}")]
    public Customer GetCustomer(int id) => _dataContext.Customers.FirstOrDefault(c => c.CustomerId == id);

    [HttpGet, Route("{id:int}/orders")]
    public IEnumerable<Order> GetOrders(int id) => _dataContext.Orders
        .Where(o => o.CustomerId == id);
    
    [HttpGet, Route("{id:int}/orderdetails")]
    public IEnumerable<OrderDetail> GetOrderDetails(int id) => _dataContext.OrderDetails
        .Where(d => d.OrderId == id)
        .Include(d => d.Product);
}