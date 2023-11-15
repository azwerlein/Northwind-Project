using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers.api;

[Route("api/product/")]
public class ProductApiController : Controller
{
    private readonly DataContext _dataContext;
    public ProductApiController(DataContext db) => _dataContext = db;
    
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
}