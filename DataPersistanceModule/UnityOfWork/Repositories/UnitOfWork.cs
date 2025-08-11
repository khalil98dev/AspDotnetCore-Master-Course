using EFCoreBasicsImplementation.Data;
using EFCoreBasicsImplementation.interfaces;

namespace EFCoreBasicsImplementation.Repositories;



public class UnitOfWork(ProductAppContext _context) : IUnitOfWork, IDisposable
{

    IProductRepository? _productRepository;

    public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }
    
}