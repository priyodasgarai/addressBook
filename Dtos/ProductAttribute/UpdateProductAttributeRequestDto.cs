namespace addressBook.Dtos.ProductAttribute
{
    public class UpdateProductAttributeRequestDto
    {      
        public string SKU { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Size { get; set; }
        public int StockQuantity { get; set; }
        public decimal? Price { get; set; }
        public bool IsDefault { get; set; } = false;
        public int Status { get; set; }
      
    }
}