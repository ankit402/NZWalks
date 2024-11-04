using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
    public class Region
    {
        [Key]
        public Guid Guid { get; set; }
        public string Code {  get; set; }
        public string Name { get; set; }
        public string?  RegionImageurl { get; set; }
    }
}
