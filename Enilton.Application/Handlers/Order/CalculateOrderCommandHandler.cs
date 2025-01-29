using Enilton.Application.Commands.Order;
using Enilton.Application.DTOs.Order;
using MediatR;

namespace Enilton.Application.Handlers.Order
{
    public class CalculateOrderCommandHandler : IRequestHandler<CalculateOrderCommand, CalculateOrderResultDTO>
    {
        public Task<CalculateOrderResultDTO> Handle(CalculateOrderCommand request, CancellationToken cancellationToken)
        {
            // Itera pelos itens e calcula TotalPrice
            foreach (var item in request.Request.Items)
            {
                item.TotalPrice = item.Quantity * item.UnitPrice;
            }

            // Soma o TotalPrice para calcular o TotalAmount
            var totalAmount = request.Request.Items.Sum(i => i.TotalPrice);

            // Retorna o resultado
            var result = new CalculateOrderResultDTO
            {
                TotalAmount = totalAmount,
                Items = request.Request.Items.Select(i => new OrderItemDTO
                {
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };

            return Task.FromResult(result);
        }
    }
}
