namespace Enilton.Application.DTOs.Order
{
    public class CreateOrderResultDTO
    {
        public Guid? OrderId { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
