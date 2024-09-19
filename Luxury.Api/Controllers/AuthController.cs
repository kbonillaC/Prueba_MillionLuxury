using Luxury.Api.Application.Managers.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace Luxury.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtManager _jwt;

        public AuthController(IJwtManager jwt)
        {
            _jwt = jwt;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(new { Token = _jwt.GetToken() });
        }
    }
}
