using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers.api;

[Route("api/product/")]
public class ProductApiController : Controller
{
    private readonly DataContext _dataContext;

    public ProductApiController(DataContext db)
    {
        _dataContext = db;
    }

    // returns all products (question-mark after url parameter means it's optional)
    [HttpGet, Route("{discontinued:bool?}")]
    public IEnumerable<Product> Get(bool discontinued = false) => _dataContext.Products
        .Where(p => !p.Discontinued || p.Discontinued == discontinued)
        .OrderBy(p => p.ProductName);

    // return a specific product
    [HttpGet, Route("{id:int}")]
    public Product Get(int id) => _dataContext.Products
        .FirstOrDefault(p => p.ProductId == id);

    // return a specific product's reviews
    [HttpGet, Route("{id:int}/reviews")]
    public IEnumerable<Review> GetReviews(int id) => _dataContext.Reviews
        .Where(r => r.ProductId == id);

    // add a review
    [HttpPost, Route("{id:int}/reviews")]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult PostReview(int id, [FromBody] Review review)
    {
        if (review is null)
            return StatusCode(422); // Unprocessable Content (Model is missing)

        review.ProductId = id;
        review.CustomerId = _dataContext.Customers.FirstOrDefault(
            c => c.Email == User.Identity.Name)?.CustomerId ?? -1;
        //Invalid customer id, will error when we check model state.

        if (User.IsInRole("admin")) // If user is in role, manually assign user id to Admin uid.
            review.CustomerId = int.MinValue;

        if (!_dataContext.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Order.Customer)
                .Any(od => od.ProductId == id))
            return StatusCode(401); // Unauthorized (You haven't purchased this item!)

        if (!ModelState.IsValid)
            return StatusCode(422); // Unprocessable Content (Review Model is bad)

        if (_dataContext.Reviews.Any(r => r.CustomerId == review.CustomerId && r.ProductId == review.ProductId))
            return StatusCode(409); // Conflict (Resource already exists)

        try
        {
            _dataContext.Reviews.Add(review);
            _dataContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            return StatusCode(500);
        }

        return StatusCode(200); //OK
    }
}