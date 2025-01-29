using Enilton.Application.DTOs.Order;
using MediatR;

namespace Enilton.Application.Commands.Order
{
    public class CreateOrderCommand : IRequest<CreateOrderResultDTO>
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public List<CreateOrderItemDTO> Items { get; set; }
    }
}
