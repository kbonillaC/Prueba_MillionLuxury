using Luxury.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Luxury.Domain.Models
{
    public class Property
    {
        [Key]
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int IdOwner { get; set; }


    }
}
