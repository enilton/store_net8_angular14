using Enilton.Application.Commands.Order;
using Enilton.Application.DTOs.Order;
using Enilton.Domain.Interfaces;
using MediatR;

namespace Enilton.Application.Handlers.Order
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResultDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<CreateOrderResultDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Criar o pedido
            var order = new Domain.Entities.Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                Status = Enum.Parse<Enilton.Domain.Enums.OrderStatus>(request.Status),
                TotalAmount = request.TotalAmount
            };

            await _orderRepository.AddAsync(order);

            // Criar os itens do pedido
            var items = request.Items.Select(item => new Domain.Entities.OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.Quantity * item.UnitPrice
            }).ToList();

            foreach (var item in items)
                await _orderItemRepository.AddAsync(item);

            return new CreateOrderResultDTO
            {
                OrderId = order.Id,
                Items = items.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };
        }
    }
}
