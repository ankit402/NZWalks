using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
    public class Walk
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public double  LengthInKm { get; set; }

        public string? WalkImageurl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Nevigation url
        public Difficulty difficulty { get; set; }

        public Region region { get; set; }

    }
}
