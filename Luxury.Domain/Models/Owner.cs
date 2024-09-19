using System.ComponentModel.DataAnnotations;

namespace Luxury.Domain.Models
{
    public class Owner
    {
        [Key]
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public DateTime? Birthday { get; set; }

    }
}