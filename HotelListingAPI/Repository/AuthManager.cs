﻿using AutoMapper;
using HotelListingApi.Contracts;
using HotelListingApi.Data;
using HotelListingApi.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListingApi.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            
            if(user == null || isValidUser == false)
            {
                return null;
            }

            var token = await GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id
            };


        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {

            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;
            

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;

        }

        private async Task<string> GenerateToken(ApiUser User)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(User);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(User);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, User.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, User.Email),
                new Claim("uid", User.Id)
            }
            .Union(roleClaims).Union(userClaims);

            var token = new JwtSecurityToken(

                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(token); 

        }

    }
}
