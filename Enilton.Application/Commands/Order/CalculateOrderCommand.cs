using Enilton.Application.DTOs.Order;
using Enilton.Application.Requests.Order;
using MediatR;

namespace Enilton.Application.Commands.Order
{
    public class CalculateOrderCommand : IRequest<CalculateOrderResultDTO>
    {
        public CalculateOrderRequest Request { get; set; }
    }
}
