namespace DD_Footwear.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockLevel { get; set; }
        public int Lock {  get; set; }
        public int Unlock { get; set; }
        public String Categorie { get; set; }
    }
}
