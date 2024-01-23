using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    [Key]
	public int Id { get; set; } //auto

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Email { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Password { get; set; } = null!;


    //en Customer kan ha en CustomerProfile
    public virtual CustomerProfileEntity? CustomerProfile { get; set; }
}

