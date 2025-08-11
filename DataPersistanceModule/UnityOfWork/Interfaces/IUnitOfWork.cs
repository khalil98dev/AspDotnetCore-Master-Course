using System;
using EFCoreBasicsImplementation.Data; 

namespace EFCoreBasicsImplementation.interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    Task<int> SaveChangesAsync(CancellationToken ct=default);
}
