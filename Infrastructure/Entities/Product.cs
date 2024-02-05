using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public virtual ProductDetail? ProductDetail { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
