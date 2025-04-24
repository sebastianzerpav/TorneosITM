using Microsoft.AspNetCore.Mvc;
using TorneosITM.Data.DTOs;
using TorneosITM.Services.Auth;

namespace TorneosITM.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody] AuthRequest authRequest)
        {
            AuthResponse resultado = await authService.GetToken(authRequest);
            if (resultado == null) { return Unauthorized("Credenciales inválidas."); }
            else
            {
                return Ok(resultado);
            }
        }
    }
}
