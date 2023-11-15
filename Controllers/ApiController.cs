using Microsoft.AspNetCore.Mvc;
using Northwind.Migrations;

namespace Northwind.Controllers;

[Route("api")]
public class APIController : Controller
{
    // this controller depends on the NorthwindRepository
    private DataContext _dataContext;
    public APIController(DataContext db) => _dataContext = db;

    [HttpGet, Route("product")]
    // returns all products
    public IEnumerable<Product> Get() => _dataContext.Products.OrderBy(p => p.ProductName);

    [HttpGet, Route("product/{id:int}")]
    // returns specific product
    public Product Get(int id) => _dataContext.Products.FirstOrDefault(p => p.ProductId == id);

    [HttpGet, Route("product/discontinued/{discontinued:bool}")]
    // returns all products where discontinued = true/false
    public IEnumerable<Product> GetDiscontinued(bool discontinued) => _dataContext.Products
        .Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);

    [HttpGet, Route("category/{categoryId:int}/product")]
    // returns all products in a specific category
    public IEnumerable<Product> GetByCategory(int categoryId) =>
        _dataContext.Products.Where(p => p.CategoryId == categoryId).OrderBy(p => p.ProductName);
    
    [HttpGet, Route("category/{categoryId:int}/product/discontinued/{discontinued}")]
    // returns all products in a specific category where discontinued = true/false
    public IEnumerable<Product> GetByCategoryDiscontinued(int categoryId, bool discontinued) => _dataContext.Products.Where(p => p.CategoryId == categoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);

}