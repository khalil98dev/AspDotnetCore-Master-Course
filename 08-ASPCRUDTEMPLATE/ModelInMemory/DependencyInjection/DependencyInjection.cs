
namespace Microsoft.Extensions.DependencyInjection;
using Modeles; 
public static class ProductDependencies
{
    static public IServiceCollection AddProductService(this IServiceCollection Service)
    {
        Service.AddSingleton<ProductStore>();
        return Service;
    }
}