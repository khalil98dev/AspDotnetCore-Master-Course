
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

    [HttpGet]
    public IActionResult Edit(Guid ID)
    {
        var Product = Store.GetProductByID(ID);
        if (Product == null) return NotFound($"The Specific ID {ID} it does not represent any product");
        return View(Product);
    }

    [HttpPost, ActionName("Edit")]
    public IActionResult Edit(Product newProduct)
    {
        var result = Store.Update(newProduct);
        if (!result) return NotFound();

        return RedirectToAction(nameof(Index));

    }



    [HttpGet("Details/{ID}")]
    public IActionResult Details(Guid ID)
    {
        var Product = Store.GetProductByID(ID);
        if (Product == null) return NotFound($"The Specific ID {ID} it does not represent any product");
        return View(Product);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        product.ID = Guid.NewGuid();
        Store.Add(product);
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Delete(Guid ID)
    {
        var product = Store.GetProductByID(ID);
        if (product == null) return NotFound("Product not found");
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult ConfirmDelete(Guid ID)
    {
        var product = Store.GetProductByID(ID);
        if (product == null) return NotFound("Product not found");

        Store.Delete(ID);
        return RedirectToAction(nameof(Index));
    }
}