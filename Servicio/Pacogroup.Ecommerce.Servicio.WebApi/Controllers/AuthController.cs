using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Pacogroup.Ecommerce.Services.WebApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones de Autenticación")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;

        public AuthController(IAuthApplication authApplication)
        {
            _authApplication = authApplication;
        }

        [HttpPost("signup")]
        [SwaggerOperation(Summary = "Registra un nuevo usuario")]
        public async Task<IActionResult> SignUpAsync([FromBody] SingUpDTO signUpDto)
        {
            var response = await _authApplication.SignUpAsync(signUpDto);

            if (response.IsSucces)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("signin")]
        [SwaggerOperation(Summary = "Autentica un usuario y genera token")]
        public async Task<IActionResult> SignInAsync([FromBody] SingInDTO signInDto)
        {
            var response = await _authApplication.SignInAsync(signInDto);

            if (response.IsSucces)
                return Ok(response);

            return Unauthorized(response);
        }
    }
}