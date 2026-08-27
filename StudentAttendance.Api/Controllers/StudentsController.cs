using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using System.Security.Claims;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// API controller for managing students (create, read, update, delete).
    /// </summary>
    /// 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentsController"/> class.
        /// </summary>
        /// <param name="studentService">Service that handles student operations.</param>

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
        {
            var studentId = await _studentService.CreateStudentAsync(dto);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = studentId },
                new
                {
                    Id = studentId,
                    Message = "Student created successfully."
                });
        }

        /// <summary>
        /// Gets all students.
        /// </summary>
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return Ok(students);
        }

        /// <summary>
        /// Gets a student by ID.
        /// </summary>
        /// <param name="id">The student ID.</param>

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound(new
                {
                    Message = "Student not found."
                });
            }

            return Ok(student);
        }


        [Authorize(Roles = "Student")]
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

            var student =
                await _studentService.GetStudentByUserIdAsync(userId);

            if (student == null)
            {
                return NotFound(new
                {
                    Message = "Student profile not found."
                });
            }

            return Ok(student);
        }

        /// <summary>
        /// Updates an existing student.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// /// <param name="dto">The update data for the student.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(    int id,    UpdateStudentDto dto)
        {
            var updated = await _studentService.UpdateStudentAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = "Student not found."
                });
            }

            return Ok(new
            {
                Message = "Student updated successfully."
            });
        }

        /// <summary>
        /// Deletes an existing student.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    Message = "Student not found."
                });
            }

            return Ok(new
            {
                Message = "Student deleted successfully."
            });
        }


    }
}
