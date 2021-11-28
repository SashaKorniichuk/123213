using Domain.ApiModel.RequestApiModels;
using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _serviceAccount;

        public AuthController(IAuthService serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestApiModel model)
        {
            var response = await _serviceAccount.LoginUser(model);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestApiModel userRegisterRequest)
        {
            var response = await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok(response);
        }
    }
}
