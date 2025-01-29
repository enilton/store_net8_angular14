using Enilton.Application.DTOs.Order;

namespace Enilton.Application.Requests.Order
{
    public class CalculateOrderRequest
    {
        public List<CalculateDTO> Items { get; set; }
    }
}
