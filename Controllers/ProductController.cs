using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers;

public class ProductController : Controller
{
    // this controller depends on the NorthwindRepository
    private DataContext _dataContext;
    public ProductController(DataContext db) => _dataContext = db;
    public IActionResult Category() => View(_dataContext.Categories.OrderBy(c => c.CategoryName));

    public IActionResult Index(int id)
    {
        ViewBag.id = id;
        return View(_dataContext.Categories.OrderBy(c => c.CategoryName));
    }

    public IActionResult Reviews(int id) => View(id);

}