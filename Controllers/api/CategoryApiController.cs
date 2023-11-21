using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

namespace Northwind.Controllers.api;

[Route("api/category/")]
public class CategoryApiController : Controller
{
    private readonly DataContext _dataContext;
    public CategoryApiController(DataContext db) => _dataContext = db;

    // returns all categories
    [HttpGet, Route("")]
    public IEnumerable<Category> Get() => _dataContext.Categories
        .Include(c => c.Products)
        .OrderBy(c => c.CategoryName);
    
    // returns a specific category
    [HttpGet, Route("{id:int}")]
    public Category Get(int id) => _dataContext.Categories
        .FirstOrDefault(c => c.CategoryId == id);
    
    // returns all products within a category (question-mark after url parameter means it's optional)
    [HttpGet, Route("{id:int}/products/{discontinued:bool?}")]
    public IEnumerable<Product> GetProducts(int id, bool discontinued = false) => _dataContext.Products
        .Where(p => p.Discontinued == discontinued)
        .Where(p => p.CategoryId == id);
}