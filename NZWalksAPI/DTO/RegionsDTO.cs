using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.DTO
{
    public class RegionsDTO
    {
        [Key]
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageurl { get; set; }
    }
}
