using Domain.ApiModel.RequestApiModels;
using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestApiModel data)
        {
            var userId = User.FindFirst("Id").Value;
            var response = await _accountService.ChangePassword(data.NewPassword, data.OldPassword, userId);
            
            return Ok(response);
        }

        [HttpPost("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequestApiModel data)
        {
            var userId = User.FindFirst("Id").Value;
            var response = await _accountService.ChangeEmail(data.NewEmail, userId);

            return Ok(response);
        }
        [HttpGet("PersonalInformation")]
        public async Task<IActionResult> GetPersonalInformation()
        {
            var userId = User.FindFirst("Id").Value;
            var response = await _accountService.GetPersonalInformation(userId);

            return Ok(response);
        }
    }
}
