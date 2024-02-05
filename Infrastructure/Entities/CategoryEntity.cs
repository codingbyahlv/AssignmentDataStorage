namespace Infrastructure.Entities;

public partial class CategoryEntity
{
    public int Id { get; set; }

    public int? ParentCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<CategoryEntity> InverseParentCategory { get; set; } = new List<CategoryEntity>();

    public virtual CategoryEntity? ParentCategory { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
