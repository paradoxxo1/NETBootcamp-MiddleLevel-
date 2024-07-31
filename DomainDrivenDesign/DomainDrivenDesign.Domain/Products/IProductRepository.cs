﻿namespace DomainDrivenDesign.Domain.Products;
public interface IProductRepository
{
    Task CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> IsNameExists(Name name, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
}
