using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreatePropertyCommand : IRequest<LuxuryResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public long? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int IdOwner { get; set; }

        public CreatePropertyCommand(string name, string address, long? price, int? codeInternal, int? year, int idOwner)
        {
            Name = name;
            Address = address;
            Price = price;
            CodeInternal = codeInternal;
            Year = year;
            IdOwner = idOwner;
        }
    }
}
