using System.ComponentModel.DataAnnotations;

namespace Luxury.Domain.Models
{
    public class PropertyTrace
    {
        [Key]
        public int IdPropertyTrace { get; set; }
        public DateTime? DateSale { get; set; }
        public string Name { get; set; }
        public long? Value { get; set; }
        public decimal? Tax { get; set; }
        public int IdProperty { get; set; }

    }

}
