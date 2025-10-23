using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class AuthenticationController(IServiceManger _serviceManger): ApiBaseController
    {
        // LogIn 
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LogIn(LogInDto loginDto)
        {
            var user = await _serviceManger.AuthenticationServices.LogInAsync(loginDto);
            return Ok(user);
        }

        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManger.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }

        // Check Email
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManger.AuthenticationServices.CheckEmailAsync(email);
            return Ok(result);
        }


        // Get Current User
        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var appUser = await _serviceManger.AuthenticationServices.GetCurrentUserAsync(email!);
            return Ok(appUser);
        }
        // Get Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address= await _serviceManger.AuthenticationServices.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }
        // Update Current User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAdress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress =  await _serviceManger.AuthenticationServices.UpdateCurrentUserAddressAsync(email!, address);
            return Ok(updatedAddress);
        }
    }
}
