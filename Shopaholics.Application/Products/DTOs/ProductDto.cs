namespace Shopaholics.Application.Products.DTOs
{
    public class ProductApiResponse
    {
        public List<ProductDto> Products { get; set; } = [];
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string Thumbnail { get; set; } = default!;
    }
}
