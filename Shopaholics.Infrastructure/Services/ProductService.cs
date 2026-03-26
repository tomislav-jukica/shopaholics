using Microsoft.Extensions.Caching.Memory;
using Shopaholics.Application.Products.DTOs;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Domain.Products;
using System.Net.Http.Json;

namespace Shopaholics.Infrastructure.Services
{
    public class ProductService(HttpClient httpClient, IMemoryCache memoryCache) : IProductService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IMemoryCache _cache = memoryCache;

        public async Task<Product?> GetProductAsync(int id)
        {
            string cacheKey = $"product_{id}";

            if (!_cache.TryGetValue(cacheKey, out Product product))
            {
                var p = await _httpClient.GetFromJsonAsync<ProductDto>($"https://dummyjson.com/products/{id}");
                if (p == null) return null;

                product = new Product
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Thumbnail = p.Thumbnail
                };

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(cacheKey, product, cacheOptions);
            }
                
            return product;            
        }

        public async Task<List<Product>?> GetProductsAsync()
        {
            string cacheKey = "all_products";

            if (!_cache.TryGetValue(cacheKey, out List<Product>? products))
            {
                var response = await _httpClient.GetFromJsonAsync<ProductApiResponse>("https://dummyjson.com/products");
                if (response == null) return [];

                products = response.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    Thumbnail = p.Thumbnail
                }).ToList();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(cacheKey, products, cacheOptions);
            }

            return products;
        }
    }
}
