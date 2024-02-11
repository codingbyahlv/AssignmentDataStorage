using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OrderRowEntity
{
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

    public virtual OrderEntity Order { get; set; } = null!;
}
