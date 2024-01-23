using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OrderEntity
{
    [Key]
	public int Id { get; set; } //auto

    [Required]
    [Column(TypeName = "datetime2")]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string Status { get; set; } = null!;

    [Required]
    public int CustomerId { get; set; }

    //Order måste ha en Customer
    public virtual CustomerEntity Customer { get; set; } = null!;
}

