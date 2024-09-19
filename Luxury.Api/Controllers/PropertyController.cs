using Luxury.Api.Application.Commands;
using Luxury.Api.Application.Querys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Luxury.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPropertiesByName/name={name}")]
        public async Task<IActionResult> GetPropertiesByName(string name)
        {
            var result = await _mediator.Send(new GetAllPropetiesByNameQuery(name));
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateOwner")]
        public async Task<IActionResult> CreateOwner(CreateOwnerCommand owner)
        {
            if (String.IsNullOrEmpty(owner.Name))
            {
                return BadRequest("Property Name is required");
            }
            var result = await _mediator.Send(owner);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProperty")]
        public async Task<IActionResult> CreateProperty(CreatePropertyCommand createProperty)
        {
            if ( createProperty.IdOwner is 0)
            {
                return BadRequest("Property IdOwner is required");
            }
            var result = await _mediator.Send(createProperty);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreatePropertyImage")]
        public async Task<IActionResult> CreatePropertyImage(CreatePropertyImageCommand createPropertyImage)
        {
            if (createPropertyImage.IdProperty is 0)
            {
                return BadRequest("Property IdProperty is required");
            }
            var result = await _mediator.Send(createPropertyImage);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateOwnerAndProperty")]
        public async Task<IActionResult> CreateOwnerAndProperty(CreateOwnerAndPropertyCommand createOwnerAndProperty)
        {
            if (String.IsNullOrEmpty(createOwnerAndProperty.NameOwner))
            {
                return BadRequest("Property NameOwner is required");
            }
            var result = await _mediator.Send(createOwnerAndProperty);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreatePropertyTrace")]
        public async Task<IActionResult> CreatePropertyTrace(CreatePropertyTraceCommand createPropertyTraceCommand)
        {
            if (createPropertyTraceCommand.IdProperty is 0)
            {
                return BadRequest("Property IdProperty is required");
            }
            var result = await _mediator.Send(createPropertyTraceCommand);
            return Ok(result);
        }

        [HttpPatch]
        [Route("UpdatePrice/PropertyId={id}/price={price}")]
        public async Task<IActionResult> UpdatePrice(int id, long price)
        {
            if (id is 0)
            {
                return BadRequest("Property id is required");
            }
            var command = new UpdatePropertyPriceCommand(id, price);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProperty/PropertyId={id}")]
        public async Task<IActionResult> UpdateProperty(int id, UpdatePropertyCommand command)
        {
            if (id != command.IdProperty)
            {
                return BadRequest("Property ID mismatch");
            }

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
