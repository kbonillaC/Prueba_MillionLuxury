using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreatePropertyTraceCommand : IRequest<LuxuryResponse>
    {
        public DateTime? DateSale { get; set; }
        public string Name { get; set; }
        public long? Value { get; set; }
        public decimal? Tax { get; set; }
        public int IdProperty { get; set; }

        public CreatePropertyTraceCommand(DateTime? dateSale, string name, long? value, decimal? tax, int idProperty)
        {
            DateSale = dateSale;
            Name = name;
            Value = value;
            Tax = tax;
            IdProperty = idProperty;
        }
    }
}
