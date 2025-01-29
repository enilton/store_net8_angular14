using Enilton.Application.DTOs.Order;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public double TotalAmount { get; set; }
    public List<CreateOrderItemDTO> Items { get; set; }
}



