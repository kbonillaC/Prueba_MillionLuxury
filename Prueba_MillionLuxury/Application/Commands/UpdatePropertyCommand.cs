using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class UpdatePropertyCommand : IRequest<LuxuryResponse>
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long Price { get; set; }
        public int CodeInternal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }

        public UpdatePropertyCommand(int idProperty, string name, string address, long price, int codeInternal, int year, int idOwner)
        {
            IdProperty = idProperty;
            Name = name;
            Address = address;
            Price = price;
            CodeInternal = codeInternal;
            Year = year;
            IdOwner = idOwner;
        }
    }
}
