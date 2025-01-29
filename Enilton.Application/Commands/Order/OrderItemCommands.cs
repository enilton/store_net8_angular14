using MediatR;

namespace Enilton.Application.Commands.Order
{
    public class CreateOrderItemCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
    }

    public class UpdateOrderItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    public class DeleteOrderItemCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
