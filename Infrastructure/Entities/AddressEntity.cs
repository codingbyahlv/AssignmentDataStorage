using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class AddressEntity
    {
        [Key]
		public int Id { get; set; } //auto

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


        //en adress kan vara kopplad till flera användare
        public virtual ICollection<CustomerProfileEntity> CustomerProfile { get; set; } = new List<CustomerProfileEntity>();
    }
}
