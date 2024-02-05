using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class ProductDetail
{
    public string ProductId { get; set; } = null!;

    public int BrandId { get; set; }

    public decimal UnitPrice { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
