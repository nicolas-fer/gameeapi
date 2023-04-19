using Api.Controllers.Base;
using Application.Dtos;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto? teamDto)
        {
            var result = await _userService.LoginAsync(teamDto);

            return ApiResponse(result);
        }

        [HttpPost]
        [Route("Logout")]
        [AllowAnonymous]
        public async Task<ActionResult> LogoutAsync([FromBody] LoginDto? teamDto)
        {
            var result = await _userService.LoginAsync(teamDto);

            return ApiResponse(result);
        }
    }
}
