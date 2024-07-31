using DomainDrivenDesign.Domain.Products;
using MediatR;

namespace DomainDrivenDesign.Application.Products;
public sealed record GetAllProductsQuery() : IRequest<List<Product>>;

internal sealed class GetAllProductsQueryHandler(
    IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productRepository.GetAllAsync(cancellationToken);
        return result;
    }
}
