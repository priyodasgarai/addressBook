namespace addressBook.Dtos.Carts
{
    public class UpdateCartRequestDto
    {
        public int Quantity { get; set; }
        public bool IsOrder { get; set; } = false;
        public int Status { get; set; }
    }
}
