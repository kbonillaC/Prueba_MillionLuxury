using System.ComponentModel.DataAnnotations;

namespace Luxury.Domain.Models
{
    public class PropertyImage
    {
        [Key]
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public byte[] FileImage { get; set; }
        public bool? Enable { get; set; }

    }
}
