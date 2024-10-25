namespace DD_Footwear.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime CreateTime { get; set; }
        public string OrderStatus { get; set; }
        public double TotalAmount { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public List<OrderItemDto> items { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    public class CreateOrderDto
    {
        public int UserID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }
}
