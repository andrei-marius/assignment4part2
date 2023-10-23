using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductsController(IDataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public IActionResult GetProducts(string? name = null)
    {

        if (name != null)
        {
            var categoriesX = _dataService.GetProductByName(name);
            return Ok(categoriesX);
        }
        var categories = _dataService.GetProducts().Select(x => CreateProductModel(x));
        return Ok(categories);
    }

    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(CreateProductModel(product));
    }

    [HttpGet("category/{id}")]
    public IActionResult GetProductsCategory(int id)
    {
        var products = _dataService.GetProducts().Where(product => product.Category.Id == id).Select(x => CreateProductModel(x));
        if (products.ToList().Count() == 0)
        {
            return NotFound(products);
        }

        return Ok(products);
    }

    [HttpGet("name")]
    public IActionResult GetProductsByName(string name)
    {
        var products = _dataService.GetProducts()
            .Where(product => product.Name.Contains(name))
            .Select(x => CreateProductModel(x));
        if (products.ToList().Count() == 0) // WIP
        {
            return NotFound(products);
        }

        return Ok(products);
    }


    private ProductModel CreateProductModel(Product product)
    {
        return new ProductModel
        {
            //Url = $"http://localhost:5001/api/categories/{category.Id}",
            Url = GetUrl(nameof(GetProduct), new { product.Id }),
            Name = product.Name,
            Category = product.Category,
            CategoryName = product.Category.Name
        };
    }

    private string? GetUrl(string name, object values)
    {
        return _linkGenerator.GetUriByName(HttpContext, name, values);
    }

}
