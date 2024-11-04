using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
    public class Difficulty
    {
        [Key]
        public Guid Id { get; set; }
        public string name { get; set; }
    }
}
