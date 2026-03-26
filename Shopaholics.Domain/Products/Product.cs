namespace Shopaholics.Domain.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string Thumbnail { get; set; } = default!;
    }
}

