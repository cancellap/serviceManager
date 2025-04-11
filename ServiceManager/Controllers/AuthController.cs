using Microsoft.AspNetCore.Mvc;
using SM.Application.DTOs;
using SM.Application.Interfaces;

namespace ServiceManager.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid client request");
            }
            var tokenGerado = await _tokenService.GenerateToken(loginDto);
            return tokenGerado;
        }
    }
}
