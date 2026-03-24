using Shopaholics.Application.Products.DTOs;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Domain.Products;
using System.Net.Http.Json;

namespace Shopaholics.Infrastructure.Services
{
    public class ProductService(HttpClient httpClient) : IProductService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ProductApiResponse>("https://dummyjson.com/products");
            if (response == null) return [];

            return response.Products.Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                Thumbnail = p.Thumbnail
            }).ToList();
        }
    }
}
