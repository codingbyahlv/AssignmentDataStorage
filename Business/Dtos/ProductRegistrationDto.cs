namespace Business.Dtos;

public class ProductRegistrationDto
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set;} = null!;
    public decimal UnitPrice {  get; set; }
    public string? Color { get; set; }
    public string? Size { get; set; }
    public string Brand { get; set; } = null!;
    public string Category { get; set; } = null!;
    public int ParentCategoryId {  get; set; }
}
