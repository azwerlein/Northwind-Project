using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers;

public class HomeController : Controller
{
    // this controller depends on the NorthwindRepository
    private DataContext _dataContext;
    public HomeController(DataContext db) => _dataContext = db;
    public ActionResult Index() => View(_dataContext.Discounts.Where(d => d.StartTime <= DateTime.Now && d.EndTime > DateTime.Now).Take(3));
}