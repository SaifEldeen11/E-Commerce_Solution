using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction;
using Shared.Dtos.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Domain.Models.IdentityModule;
using Domain.Exeptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ServiceImplemntation
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationServices
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
                throw new UserNotFoundExeption(email);

            return new UserDto()
            {
                Email = user.Email!,
                DisplayName = user.UserName ?? string.Empty,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                throw new UserNotFoundExeption(email);

            if (user.Address is null)
                throw new AddressNotFoundException(user.UserName!);

            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user is null)
                throw new UserNotFoundExeption(email);

            if (user.Address is not null)
            {
                // update 
                user.Address.FirstName= addressDto.FirstName;
                user.Address.LastName= addressDto.LastName;
                user.Address.Street= addressDto.Street;
                user.Address.City= addressDto.City;
                user.Address.Country= addressDto.Country;
            }
            else
            {
                // create new address
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }

             await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<UserDto> LogInAsync(LogInDto logInDto)
        {
            // check if email is exists
            var user = await _userManager.FindByEmailAsync(logInDto.Email);

            if (user is null)
            {
                throw new UserNotFoundExeption(logInDto.Email);
            }

            // check password

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);

            if (isPasswordValid)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.UserName ?? string.Empty,
                    Token =  await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // mapping Register Dto to ApplicationUser

            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            // Create User
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.UserName ?? string.Empty,
                    Token =  await CreateTokenAsync(user)
                };


            }
            else
            {
                throw new BadRequestException(result.Errors.Select(e => e.Description).ToList());
            }
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles =  await _userManager.GetRolesAsync(user) ;

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
