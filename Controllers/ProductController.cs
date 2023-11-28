using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers;

public class ProductController : Controller
{
    // this controller depends on the NorthwindRepository
    private readonly DataContext _dataContext;
    public ProductController(DataContext db) => _dataContext = db;
    public IActionResult Category() => View(_dataContext.Categories.OrderBy(c => c.CategoryName));

    public IActionResult Index(int id)
    {
        ViewBag.id = id;
        return View(_dataContext.Categories.OrderBy(c => c.CategoryName));
    }

    public IActionResult Reviews(int id) => View(_dataContext.Products
        .Include(p => p.Reviews)
        .FirstOrDefault(p => p.ProductId == id));

    public IActionResult AddReview(int id) => View(_dataContext.Products
        .FirstOrDefault(p => p.ProductId == id));

    [Authorize(Roles = "admin")]
    public IActionResult DeleteReview(int pid, int cid)
    {
        var review = _dataContext.Reviews.FirstOrDefault(r => r.ProductId == pid &&
                                                              r.CustomerId == cid);

        if (review is null)
            return StatusCode(422);

        try
        {
            _dataContext.Reviews.Remove(review);
            _dataContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500);
        }
        return RedirectToAction("Reviews", new { id = pid });
    }
}