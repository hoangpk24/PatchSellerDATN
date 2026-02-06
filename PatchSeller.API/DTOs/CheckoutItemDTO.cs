namespace PatchSeller.API.DTOs
{
    public class CheckoutItemDTO
    {
       
            public int PatchId { get; set; }
            public double Price { get; set; }
            public string? PatchName { get; set; }
            public string? GameName { get; set; }


    }
}
