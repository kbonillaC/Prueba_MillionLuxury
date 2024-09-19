using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Luxury.Api.Application.Querys
{
    public class GetAllPropetiesByNameQueryHandler : IRequestHandler<GetAllPropetiesByNameQuery, LuxuryResponse>
    {

        private readonly MillionDbContext _context;

        public GetAllPropetiesByNameQueryHandler(MillionDbContext context)
        {
            _context = context;
        }

        public async Task<LuxuryResponse> Handle(GetAllPropetiesByNameQuery request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "No data found",
                Success = false
            };
            try
            {
                var properties = await _context.Property.Where(p => p.Name == request.Name).ToListAsync(cancellationToken);

                response.Success = properties.Count > 0 ? true: false;
                response.Message = properties.Count > 0 ? "data found" : "No data found";
                response.Data = properties.Count > 0 ? properties : null;
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
