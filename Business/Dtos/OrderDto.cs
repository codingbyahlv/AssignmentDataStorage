

namespace Business.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = null!;
    public int CustomerId { get; set; }
}
