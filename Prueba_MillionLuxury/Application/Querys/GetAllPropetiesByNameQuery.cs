using Luxury.Domain.Models;
using MediatR;

namespace Luxury.Api.Application.Querys
{
    public class GetAllPropetiesByNameQuery : IRequest<LuxuryResponse>
    {
        public string Name { get; set; }

        public GetAllPropetiesByNameQuery(string name)
        {
            Name = name;
        }
    }
}
