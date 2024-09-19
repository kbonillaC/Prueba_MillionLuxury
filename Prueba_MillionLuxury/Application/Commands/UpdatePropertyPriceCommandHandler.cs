using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class UpdatePropertyPriceCommandHandler : IRequestHandler<UpdatePropertyPriceCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;

        public UpdatePropertyPriceCommandHandler(MillionDbContext context)
        {
            _context = context;
        }
        public async Task<LuxuryResponse> Handle(UpdatePropertyPriceCommand request, CancellationToken cancellationToken)
        {

            var response = new LuxuryResponse()
            {
                Success = false,
                Message = "Failed to update property price"
            };

            try
            {
                var property = await _context.Property.FindAsync(request.PropertyId);
                if (property != null)
                {
                    property.Price = request.Price;

                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Property price updated successfully";
                    response.Data = property;
                }
                else
                {
                    response.Message = "Property not found";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
