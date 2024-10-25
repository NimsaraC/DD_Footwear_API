using DD_Footwear.Models;

namespace DD_Footwear.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<StockItemsDto> Stocks { get; set; }
    }
    public class StockItemsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public double StockPrice { get; set; }
        public int StockId { get; set; }
    }
    public class AddStockItemDto
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public double StockPrice { get; set; }
        //public int StockId { get; set; }
    }

}
