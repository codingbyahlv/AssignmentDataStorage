using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    //Id int not null identity primary key, 
    //Email nvarchar(100) not null,
    //Password nvarchar(100) not null

    //en Customer måste ha en CustomerProfile

    [Key]
	public int Id { get; set; }

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Email { get; set; } = null!;

	[Required]
	[Column(TypeName = "nvarchar(100)")]
	public string Password { get; set; } = null!;


    //defininera en till en relation - en Customer kan ha en CustomerProfile
    public virtual CustomerProfileEntity? CustomerProfile { get; set; }
}


//hur gör id auto i DB? blir det automatiskt?
