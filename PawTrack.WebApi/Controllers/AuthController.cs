using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawTrack.Business.Operations.Appointment;
using PawTrack.Business.Operations.User;
using PawTrack.Business.Operations.User.Dtos;
using PawTrack.Data.Enums;
using PawTrack.WebApi.Jwt;
using PawTrack.WebApi.Models;

namespace PawTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addUserDto = new AddUserDto
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucceed)
                return Ok();
            else 
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _userService.LoginUser(new LoginUserDto
            {
                Email = request.Email, 
                Password = request.Password
            });

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse {
                Message = "Giris basarilidir",
                Token = token
            });

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok(result.Message);
            }
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateRole(int id, UserType newUserType)
        {
            var result = await _userService.UpdateRole(id, newUserType);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
