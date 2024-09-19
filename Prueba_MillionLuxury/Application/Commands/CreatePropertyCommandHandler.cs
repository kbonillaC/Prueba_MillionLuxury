using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;

        public CreatePropertyCommandHandler(MillionDbContext context)
        {
            _context = context;
        }

        public async Task<LuxuryResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "Fail Property creation",
                Success = false
            };
            try
            {
                Property property = new()
                {
                    Name = request.Name,
                    Address = request.Address,
                    Price = request.Price,
                    CodeInternal = request.CodeInternal,
                    Year = request.Year,
                    IdOwner = request.IdOwner,
                };

                _context.Property.Add(property);
                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Correct Property creation";
                response.Data = property.IdProperty;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }

            return response;


        }
    }
}
