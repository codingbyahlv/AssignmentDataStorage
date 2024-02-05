namespace Infrastructure.Entities;

public partial class BrandEntity
{
    public int Id { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<ProductDetailEntity> ProductDetails { get; set; } = new List<ProductDetailEntity>();
}
