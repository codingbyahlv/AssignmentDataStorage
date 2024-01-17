using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OrderRowEntity
{
    //RowId int not null,
    //OrderId int not null references Orders(Id),
    //ProductId nvarchar(100) not null references Products(Id),
    //ProductQty int not null,
    //UnitPrice money not null
    //primary key(RowId, OrderId)

    //en OrderRow måste ha en Order

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int RowId { get; set; }

    [Key]
    public int OrderId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string ProductId { get; set; } = null!;

    [Required]
    public int ProductQty { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal UnitPrice { get; set; }

    //defininera en till en relation - orderRow måste ha en Order
    public virtual OrderEntity Order { get; set; } = null!;
}
