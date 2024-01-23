namespace Infrastructure.Dtos;

public class CustomerDto
{ 
    public int Id { get; set; }
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = null!;
    public int? AddressId { get; set; }
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; }  
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}
