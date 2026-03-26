using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>?> GetProductsAsync();
        Task<Product?> GetProductAsync(int id);
    }
}
