namespace Business.Dtos;

public class ProductDto
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; } = null!;
    public int CategoryId { get; set; }
    public int ParentCategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
}
