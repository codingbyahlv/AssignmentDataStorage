using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OrderEntity
{
    //Id int not null identity primary key,
    //Date datetime2 not null, 
    //Status nvarchar(20) not null,
    //CustomerId int not null references Customers(Id)

    //en Order måste ha en Customer

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

    //defininera en till en relation - Order måste ha en Customer
    public virtual CustomerEntity Customer { get; set; } = null!;
}

