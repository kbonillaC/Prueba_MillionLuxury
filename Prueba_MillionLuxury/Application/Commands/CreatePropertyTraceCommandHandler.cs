using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreatePropertyTraceCommandHandler : IRequestHandler<CreatePropertyTraceCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;

        public CreatePropertyTraceCommandHandler(MillionDbContext context)
        {
            _context = context;
        }

        public async Task<LuxuryResponse> Handle(CreatePropertyTraceCommand request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "Fail PropertyTrace creation",
                Success = false
            };
            try
            {
                PropertyTrace propertyTrace = new()
                {
                    DateSale = request.DateSale,
                    Name = request.Name,
                    Value = request.Value,
                    Tax = request.Tax,
                    IdProperty = request.IdProperty,
                };

                _context.PropertyTrace.Add(propertyTrace);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Correct PropertyTrace creation";
                response.Data = propertyTrace.IdPropertyTrace;


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
