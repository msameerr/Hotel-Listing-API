﻿using AutoMapper;
using HotelListingApi.Core.Contracts;
using HotelListingApi.Data;
using HotelListingApi.Core.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace HotelListingApi.Core.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration,
            ILogger<AuthManager> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
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

            _logger.LogInformation($"Token Generated for User with Email : {loginDto.Email} | Token : {token}");

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                RefreshToken = await CreateRefreshToken(user)
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

        public async Task<string> CreateRefreshToken(ApiUser user)
        {

            await _userManager.RemoveAuthenticationTokenAsync(user, "HotelListingApi", "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "HotelListingApi", "RefreshToken");
            var result = await _userManager.SetAuthenticationTokenAsync(user,"HotelListingApi", "RefreshToken", newRefreshToken);

            return newRefreshToken;

        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(user, "HotelListingApi", "RefreshToken",
                request.RefreshToken);

            if(isValidRefreshToken)
            {
                var token = await GenerateToken(user);

                return new AuthResponseDto
                {
                    Token = token,
                    UserId = user.Id,
                    RefreshToken = await CreateRefreshToken(user)
                };

            }

            await _userManager.UpdateSecurityStampAsync(user);
            return null;
        }

    }
}
