using E_Commerce.Domain.Enums.Products;

namespace E_Commerce.Domain.Entities.Products;

public class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double DiscountPercent { get; set; }

    public bool IsOnSale => DiscountPercent > 0;

    public int TotalSold { get; set; } = 0;

    public int Stock { get; set; }

    public bool IsSponsored { get; set; } = false;

    public DateTime? SponsoredUntil { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Rating> Ratings { get; set; } = new List<Rating>();

    public Category Category { get; set; }
}

