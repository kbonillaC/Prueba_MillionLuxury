using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Luxury.Api.Application.Commands
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;

        public UpdatePropertyCommandHandler(MillionDbContext context)
        {
            _context = context;
        }
        public async Task<LuxuryResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse
            {
                Message = "Property update failed",
                Success = false
            };

            try
            {
                var property = await _context.Property.FirstOrDefaultAsync(p => p.IdProperty == request.IdProperty, cancellationToken);

                if (property == null)
                {
                    response.Message = "Property not found";
                    return response;
                }

                property.Name = request.Name;
                property.Address = request.Address;
                property.Price = request.Price;
                property.CodeInternal = request.CodeInternal;
                property.Year = request.Year;
                property.IdOwner = request.IdOwner;

                _context.Property.Update(property);
                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Property updated successfully";
                response.Data = property;
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
            }

            return response;
        }
    }
}
