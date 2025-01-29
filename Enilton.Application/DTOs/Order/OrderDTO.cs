namespace Enilton.Application.DTOs.Order
{
    public class OrderDTO
    {
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
