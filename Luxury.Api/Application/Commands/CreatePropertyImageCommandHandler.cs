using Infrastructure.Context;
using Luxury.Api.Application.Managers.Property;
using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;
        private readonly IPropertyManager _propertyManager;

        public CreatePropertyImageCommandHandler(MillionDbContext context,IPropertyManager propertyManager)
        {
            _context = context;
            _propertyManager = propertyManager;
        }

        public async Task<LuxuryResponse> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "Fail PropertyImage creation",
                Success = false
            };
            try
            {
                byte[] photoProperty = _propertyManager.getByteImage(request.FileImage);

                PropertyImage image = new()
                {
                    FileImage = photoProperty,
                    Enable = request.Enable,
                    IdProperty = request.IdProperty,
                };
                _context.PropertyImage.Add(image);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Correct PropertyImage creation";
                response.Data = image;
                
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
