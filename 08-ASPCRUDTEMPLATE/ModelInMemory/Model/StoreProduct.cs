
using Microsoft.AspNetCore.Http.Features;

namespace Modeles;

public class ProductStore
{
    public enum eMode { eAddNew = 0, eUpdate = 1 }

    public eMode Mode { get; set; }

    private readonly List<Product> _Products =
    [
        new Product(Guid.NewGuid(),"Soda",15000.02m), 
        new Product(Guid.NewGuid(),"Pizza carre",1200.03m)
    ];

    public IEnumerable<Product> GetAllProducts() => _Products;

    public Product? GetProductByID(Guid ProductID) => _Products.FirstOrDefault(p => p.ID == ProductID);



    public void Add(Product item) => _Products.Add(item);

    public bool Update(Product newProduct)
    {
        var FoundItem = _Products.FirstOrDefault(p => p.ID == newProduct.ID);

        if (FoundItem == null) return false;

        FoundItem.Name = newProduct.Name;
        FoundItem.Price = newProduct.Price;

        return true;

    }

    public bool Delete(Guid ID)
    {
        var FoundItem = _Products.FirstOrDefault(p => p.ID == ID);
        if (FoundItem == null) return false;
        return FoundItem != null && _Products.Remove(FoundItem);          


    }
}
