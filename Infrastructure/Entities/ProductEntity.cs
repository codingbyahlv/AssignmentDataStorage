namespace Infrastructure.Entities;

public partial class ProductEntity
{
    public string Id { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public virtual ProductDetailEntity? ProductDetail { get; set; }

    public virtual ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
}
