namespace FakturyPro.Data.Models
{
    public class ProductDocument
    {
        public int ProductId { get; set; }
        public int DocumentId { get; set; }
        public double Quantity { get; set; }

        public Product Product { get; set; }
        public Document Document { get; set; }
    }
}