using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using StudentAttendance.Domain.Entities;


namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// Controller that exposes authentication endpoints for registering and logging in users.
    /// Supports Admin-only registration and anonymous login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Authentication service used to manage user registration and login.</param>
        public AuthController(
            IAuthService authService,
            UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }
        /// <summary>
        /// Registers an Admin, Student or Teacher.
        /// Only an authenticated Admin can use this endpoint.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(
            RegisterUserDto dto)
        {
            var result =
                await _authService.RegisterUserAsync(dto);

            return Ok(result);
        }


        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto dto)
        {
            var result =
                await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid email or password."
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User not found."
                });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description));

                return BadRequest(new
                {
                    Message = errors
                });
            }

            return Ok(new
            {
                Message = $"User {email} deleted successfully."
            });
        }
    }
}


