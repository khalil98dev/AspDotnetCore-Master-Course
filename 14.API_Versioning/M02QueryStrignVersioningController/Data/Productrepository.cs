namespace Date;
using System.Collections.Generic;
using Modele;
public class ProductRepository
{
    public List<Product> _AllProudcts = new List<Product>
    {
        new Product {ID = Guid.Parse("a3f1c2d4-5b6e-4f7a-8c9d-1e2f3a4b5c6d"),Name =  "Soda", Price = 14.05m},
        new Product {ID = Guid.Parse("b7e8f9a0-1c2d-3e4f-5a6b-7c8d9e0f1a2b"),Name =  "Pizza", Price = 145.05m}
    };


    public Product GetProduct(Guid productId)
    {
        return _AllProudcts.FirstOrDefault(p => p.ID == productId) ?? new Product();
    }   
    

}