using Shared.Dtos.IdentityModule;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationServices
    {
        // Log In
        // take email , password
        // return Token , email, password
        Task<UserDto> LogInAsync(LogInDto logInDto);

        //Register
        // Take email , password ,  userName , DisplayName , PhoneNumber
        // return Token , email, password

        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        // Check Email
        // take name
        // return bool
        Task<bool> CheckEmailAsync(string email);

        // Get Current User
        // take email
        // return userdto
        Task<UserDto> GetCurrentUserAsync(string email);

        // Get Current User Address
        // take email
        // return address dto
        Task<AddressDto> GetCurrentUserAddressAsync(string email);


        // Update Current User Address
        // email , address dto
        // return address dto
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);
    }
}
