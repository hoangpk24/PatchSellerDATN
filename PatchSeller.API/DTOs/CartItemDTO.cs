namespace PatchSeller.API.DTOs
{
    public class CartItemDTO
    {
       public int UserId { get; set; }
       public int CartId { get; set; }
       public int PatchId { get; set; }
    }
}
