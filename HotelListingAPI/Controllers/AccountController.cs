using HotelListingApi.Core.Contracts;
using HotelListingApi.Data;
using HotelListingApi.Core.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }


        // POST : api/Account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Register(ApiUserDto apiUserDto)
        {

            _logger.LogInformation($"Registration attempt for {apiUserDto.Email}");


            try
            {
                var errors = await _authManager.Register(apiUserDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something Went wrong in the {nameof(Register)}");
                throw;
            }

            
        }


        // POST : api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> Login(LoginDto loginDto)
        {

            _logger.LogInformation($"Login Attempt for {loginDto.Email}");

            try
            {

                var AuthResponce = await _authManager.Login(loginDto);

                if (AuthResponce == null)
                {
                    return Unauthorized();
                }

                return Ok(AuthResponce);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                throw;
            }

    
        }


        // POST : api/Account/refreshToken
        [HttpPost]
        [Route("refreshToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult> RefreshToken(AuthResponseDto request)
        {

            var AuthResponce = await _authManager.VerifyRefreshToken(request);

            if (AuthResponce == null)
            {
                return Unauthorized();
            }

            return Ok(AuthResponce);
        }

    }
}
