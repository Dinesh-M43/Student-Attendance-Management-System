using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using System.Security.Claims;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// API controller for managing teachers (create, read, update, delete).
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeachersController"/> class.
        /// </summary>
        /// <param name="teacherService">Service that handles teacher operations.</param>

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Creates a new teacher.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(
            CreateTeacherDto dto)
        {
            var teacherId =
                await _teacherService.CreateTeacherAsync(dto);

            return CreatedAtAction(
                nameof(GetTeacherById),
                new { id = teacherId },
                new
                {
                    Id = teacherId,
                    Message = "Teacher created successfully."
                });
        }

        /// <summary>
        /// Gets all teachers.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers =
                await _teacherService.GetAllTeachersAsync();

            return Ok(teachers);
        }

        /// <summary>
        /// Gets a teacher by ID.
        /// </summary>
        /// <param name="id">The teacher ID.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher =
                await _teacherService.GetTeacherByIdAsync(id);

            if (teacher == null)
            {
                return NotFound(new
                {
                    Message = "Teacher not found."
                });
            }

            return Ok(teacher);
        }

        /// <summary>
        /// Gets the profile of the currently logged-in teacher.
        /// </summary>
        [Authorize(Roles = "Teacher")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new
                {
                    Message = "User identity not found."
                });
            }

            var teacher =
                await _teacherService
                    .GetTeacherByUserIdAsync(userId);

            if (teacher == null)
            {
                return NotFound(new
                {
                    Message = "Teacher profile not found."
                });
            }

            return Ok(teacher);
        }

        /// <summary>
        /// Updates an existing teacher.
        /// </summary>
        /// <param name="id">The teacher ID.</param>
        /// /// <param name="dto">The update data for the teacher.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, UpdateTeacherDto dto)
        {
            var updated =
                await _teacherService.UpdateTeacherAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = "Teacher not found."
                });
            }

            return Ok(new
            {
                Message = "Teacher updated successfully."
            });
        }

        /// <summary>
        /// Deletes an existing teacher.
        /// </summary>
        /// <param name="id">The teacher ID.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var deleted =
                await _teacherService.DeleteTeacherAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Teacher not found."
                });
            }

            return Ok(new
            {
                Message = "Teacher deleted successfully."
            });
        }
    }
}
