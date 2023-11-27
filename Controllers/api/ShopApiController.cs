using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers.api;

[Route("api/shop")]
public class ShopApiController : Controller
{
    private readonly DataContext _dataContext;

    public ShopApiController(DataContext data) => _dataContext = data;
    
    [HttpPost, Route("addtocart")]
    // adds a row to the cartitem table
    public CartItem Post([FromBody] CartItemJSON cartItem) => _dataContext.AddToCart(cartItem);
}