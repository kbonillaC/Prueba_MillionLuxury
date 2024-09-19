using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreatePropertyImageCommand : IRequest<LuxuryResponse>
    {
        public string FileImage { get; set; }
        public bool? Enable { get; set; }
        public int IdProperty { get; set; }

        public CreatePropertyImageCommand(string fileImage, bool? enable, int idProperty)
        {
             FileImage = fileImage;
             Enable = enable;
             IdProperty = idProperty;
        }
    }
}
