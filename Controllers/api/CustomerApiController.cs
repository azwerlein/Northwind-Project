using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Microsoft.AspNet.Identity;

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
}