using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Models;

public class ProductModel
{
    public string Url { get; set; }
    public string? Name { get; set; }
    public string? CategoryName {  get; set; }
    public Category? Category { get; set; }
}
