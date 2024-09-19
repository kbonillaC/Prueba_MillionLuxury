using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class UpdatePropertyPriceCommand : IRequest<LuxuryResponse>
    {
        public int PropertyId { get; set; }
        public long Price { get; set; }

        public UpdatePropertyPriceCommand(int propertyId, long price)
        {
            PropertyId = propertyId;
            Price = price;
        }
    }
}
