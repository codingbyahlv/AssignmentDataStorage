namespace Infrastructure.Entities;

public partial class ProductDetailEntity
{
    public string ProductId { get; set; } = null!;

    public int BrandId { get; set; }

    public decimal UnitPrice { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public virtual BrandEntity Brand { get; set; } = null!;

    public virtual ProductEntity Product { get; set; } = null!;
}
