namespace DD_Footwear.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<StockItems> StockItems { get; set; }
    }

    public class StockItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public double StockPrice { get; set; }
        public int StockId { get; set; }
    }
}
