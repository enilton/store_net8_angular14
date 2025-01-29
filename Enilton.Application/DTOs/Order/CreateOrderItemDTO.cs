namespace Enilton.Application.DTOs.Order
{
    public class CreateOrderItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
