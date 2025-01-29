namespace Enilton.Application.DTOs.Order
{
    public class CalculateOrderResultDTO
    {
        public double TotalAmount { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
