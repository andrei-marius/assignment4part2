using DataLayer;

namespace WebServer.Models;

public class ProductModel
{
    public string Url { get; set; }
    public string? Name { get; set; }
    public Category? Category { get; set; }
}
