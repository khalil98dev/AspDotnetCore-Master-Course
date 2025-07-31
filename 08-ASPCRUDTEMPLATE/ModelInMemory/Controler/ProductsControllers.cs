
using Microsoft.AspNetCore.Mvc;
using Modeles;

public class ProductsController(ProductStore Store) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var Products = Store.GetAllProducts();
        return View(Products); 
    }

}