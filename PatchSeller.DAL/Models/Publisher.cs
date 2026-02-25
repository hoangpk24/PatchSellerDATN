using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PatchSeller.DAL.Models;

public class Publisher
{
    [Key]
    public int PublisherId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public bool Delete { get; set; }
    public DateTime? CreatedAt { get; set; }

    [JsonIgnore]
    public ICollection<Game>? Games { get; set; }
}
