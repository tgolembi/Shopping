using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shopping.Services.AuthAPI.Models.DTO;
using Shopping.Services.AuthAPI.Service.IService;

namespace Shopping.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDTO _response = new ResponseDTO();

        public AuthAPIController (IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] RegistrationDTO userRegistration)
        {
            var error = await _authService.Register(userRegistration);

            if (error == null)
            {
                _response.Success = true;
                return Ok(_response);
            }

            _response.Message = error;
            return BadRequest(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login ([FromBody] LoginRequestDTO loginRequest)
        {
            var loginResponse = await _authService.Login(loginRequest);
            if (loginResponse != null && loginResponse.User != null)
            {
                _response.Success = true;
                _response.Result = loginResponse;
                return Ok(_response);
            }
            else
            {
                _response.Message = "Username and/or Password are incorrect";
                return BadRequest(_response);
            }
        }
    }
}
