using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Luxury.Api.Application.Querys
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, LuxuryResponse>
    {

        private readonly MillionDbContext _context;

        public GetAllQueryHandler(MillionDbContext context)
        {
            _context = context;
        }

        public async Task<LuxuryResponse> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "No data found",
                Success = false
            };
            try
            {
                var properties = await _context.Property.ToListAsync(cancellationToken);

                response.Success = true;
                response.Message = "data found";
                response.Data = properties;
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
