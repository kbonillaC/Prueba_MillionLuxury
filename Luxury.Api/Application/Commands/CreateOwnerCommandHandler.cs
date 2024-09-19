using Infrastructure.Context;
using Luxury.Api.Application.Managers.Property;
using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;
        private readonly IPropertyManager _propertyManager;

        public CreateOwnerCommandHandler(MillionDbContext context, IPropertyManager propertyManager)
        {
            _context = context;
            _propertyManager = propertyManager;
        }

        public async Task<LuxuryResponse> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "Fail owner creation",
                Success = false
            };
            try
            {
                byte[] photo = _propertyManager.getByteImage(request.Photo);
                Owner owner = new Owner
                {
                    Name = request.Name,
                    Address = request.Address,
                    Photo = photo,
                    Birthday = request.Birthday
                };

                _context.Owners.Add(owner);
                await _context.SaveChangesAsync(cancellationToken);
                response.Success = true;
                response.Message = "Correct owner creation";
                response.Data = owner.IdOwner;

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
