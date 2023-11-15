using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers.api;

public class CustomerApiController : Controller
{
    private readonly DataContext _dataContext;
    public CustomerApiController(DataContext db) => _dataContext = db;
}