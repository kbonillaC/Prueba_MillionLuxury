using Infrastructure.Context;
using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreateOwnerAndPropertyCommandHandler : IRequestHandler<CreateOwnerAndPropertyCommand, LuxuryResponse>
    {
        private readonly MillionDbContext _context;
        private readonly IMediator _mediator;

        public CreateOwnerAndPropertyCommandHandler(MillionDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LuxuryResponse> Handle(CreateOwnerAndPropertyCommand request, CancellationToken cancellationToken)
        {
            LuxuryResponse response = new LuxuryResponse()
            {
                Message = "Fail owner and Property creation",
                Success = false
            };
            try
            {
                LuxuryResponse responseCreateOwner = await _mediator.Send(new CreateOwnerCommand(request.NameOwner, request.AddressOwner, request.PhotoOwner, request.BirthdayOwner));

                if (responseCreateOwner.Success)
                {
                    LuxuryResponse responseCreateProperty = await _mediator.Send(new CreatePropertyCommand(request.NameProperty, request.AddressProperty, request.PriceProperty, request.CodeInternalProperty, request.YearProperty, Convert.ToInt32(responseCreateOwner.Data)));
                    
                    if (responseCreateProperty.Success)
                    {
                        var responseCreatePropertyImage = await _mediator.Send(new CreatePropertyImageCommand(request.FileImagePropertyImage, request.EnablePropertyImage, Convert.ToInt32(responseCreateProperty.Data) ));
                        return responseCreatePropertyImage;
                    }
                }

                
                return response;

               
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
