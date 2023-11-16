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
        .Where(r => r.ProductID == id);

    // add a review
    [HttpPost, Route("{id:int}/reviews")]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult PostReview(int id, Review review)
    {
        review.ProductID = id;
        review.CustomerID = _dataContext.Customers.FirstOrDefault(
            c => c.Email == User.Identity.Name)?.CustomerId ?? -1;
        //Invalid customer id, will error when we check model state.

        if (!ModelState.IsValid) 
            return StatusCode(400); //Bad Request

        _dataContext.Reviews.Add(review);
        
        return StatusCode(200); //OK
    }
}