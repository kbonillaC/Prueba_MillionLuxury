using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Commands
{
    public class CreateOwnerCommand : IRequest<LuxuryResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }

        public CreateOwnerCommand(string name, string address, string photo, DateTime birthday)
        {
            Name = name;
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }
    }
}
