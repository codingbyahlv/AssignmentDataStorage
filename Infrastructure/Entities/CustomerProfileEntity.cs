using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerProfileEntity
{
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

    public int? AddressId { get; set; }

    public virtual CustomerEntity Customer { get; set; } = null!;

    public virtual AddressEntity? Address { get; set; }
}
