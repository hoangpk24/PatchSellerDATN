using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Models
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("GameId")]
        public int GameId { get; set; }
        [JsonIgnore]
        public Game? Game { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
