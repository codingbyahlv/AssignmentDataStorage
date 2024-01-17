using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerProfileEntity
{
    //CustomerId int not null primary key references Customers(Id),
    //FirstName nvarchar(50) not null,
    //LastName nvarchar(50) not null,
    //PhoneNumber nvarchar(17) null,
    //AddressId int not null references Addresses(id)

    //en CustomerProfile måste ha en Customer

    [Key]
	[ForeignKey(nameof(CustomerEntity))]
	public int CustomerId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "nvarchar(17)")]
    public string? PhoneNumber { get; set; }

    [ForeignKey(nameof(AddressEntity))]
    public int AddressId { get; set; }


    //defininera en till en relation - en CustomerProfile måste ha en Customer
    public virtual CustomerEntity Customer { get; set; } = null!;
}
