namespace Enilton.Entity
{
    public  class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice {  get; set; }

    }
}
