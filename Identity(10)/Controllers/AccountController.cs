using Identity_10_.Services.User;
using Identity_10_.Services.User.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity_10_.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    [Authorize]
    public class AccountController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            var result = await _service.RegisterAsync(user);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailAsyncDto confirmEmail)
        {
            var result = await _service.ConfirmEmailAsync(confirmEmail);
            if (result.IsSuccess)
            {
                return Ok("Email Confirmed");
            }
            return BadRequest("Error");
        }
    }
}
