using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Invitation_App_Web_API.Enums;
using Invitation_App_Web_API.Models.RequestModels;
using Invitation_App_Web_API.Models.ViewModels;
using Invitation_App_Web_API.Services;
using Invitation_App_Web_API.Services.User;
using Invitation_App_Web_API.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Invitation_App_Web_API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login([FromQuery] LoginRequestModel model)
        {
            var serviceResult = await _userService.Login(model);

            if (serviceResult.ServiceResultType == ServiceResultType.Fail)
                return BadRequest(serviceResult.ErrorModel);

            var token = JwtUtil.CreateJwtToken(serviceResult.Data);

            return Ok(new
            {
                user = serviceResult.Data,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUser([FromBody] UserViewModel userModel)
        {
            var serviceResult = await _userService.AddUser(userModel);

            return ReturnActionResult(serviceResult);
        }
    }
}