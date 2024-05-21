using D1_Tech.Core.Dtos.GenericDtos;
using D1_Tech.Core.Dtos.PageDtos.User;
using D1_Tech.Core.Interfaces.PageInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D1_Tech.Application.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost()]
        [Route("get-login-screen")]
        public async Task<GenericResponseDto<UserLoginDto>> Login(UserLoginInputDto loginScreen)
        {
            return await _userService.Login(loginScreen);
        }

    }
}