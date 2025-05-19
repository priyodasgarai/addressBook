namespace addressBook.Dtos.Carts
{
    public class CreateCartRequestDto
    {
        public int ProductAttributeId { get; set; }
        public string? AppUserId { get; set; }
        public int Quantity { get; set; }
    }
}
