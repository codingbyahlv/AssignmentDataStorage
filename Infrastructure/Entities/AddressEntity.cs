using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class AddressEntity
    {
        //Id int not null identity primary key,
        //StreetName nvarchar(50) not null,
        //StreetNumber varchar(5) not null,
        //PostalCode char (6) not null,
        //City nvarchar(50) not null

        //en adress kan vara kopplad till flera användare


        [Key]
		public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string StreetName { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string StreetNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "char(6)")]
        public string PostalCode { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; } = null!;


        //definerar en till många relation - en adress kan vara kopplad till flera användare
        public virtual ICollection<CustomerEntity> Users { get; set; } = new List<CustomerEntity>();
    }
}
