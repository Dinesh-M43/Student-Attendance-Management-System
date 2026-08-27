using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using System.Security.Claims;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// API controller for managing attendance (create, read, update, delete).
    /// </summary>

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendancesController"/> class.
        /// </summary>
        /// <param name="attendanceService">Service that handles attendance operations.</param>

        public AttendancesController(
            IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Creates a new attendance record.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateAttendance(
            CreateAttendanceDto dto)
        {
            var attendanceId =
                await _attendanceService.CreateAttendanceAsync(dto);

            return CreatedAtAction(
                nameof(GetAttendanceById),
                new { id = attendanceId },
                new
                {
                    Id = attendanceId,
                    Message = "Attendance created successfully."
                });
        }

        /// <summary>
        /// Gets all attendance records.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllAttendances()
        {
            var attendances =
                await _attendanceService.GetAllAttendancesAsync();

            return Ok(attendances);
        }

        /// <summary>
        /// Gets an attendance record by ID.
        /// </summary>
        /// <param name="id">The attendance record ID.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendanceById(int id)
        {
            var attendance =
                await _attendanceService.GetAttendanceByIdAsync(id);

            if (attendance == null)
            {
                return NotFound(new
                {
                    Message = "Attendance not found."
                });
            }

            return Ok(attendance);
        }

        /// <summary>
        /// Gets attendance records for a specific student.
        /// </summary>
        /// <param name="studentId">The student ID.</param>
        /// 

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var attendances =
                await _attendanceService.GetByStudentIdAsync(studentId);

            return Ok(attendances);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyAttendance()
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

            var attendance =
                await _attendanceService
                    .GetAttendanceByUserIdAsync(userId);

            return Ok(attendance);
        }

        /// <summary>
        /// Gets attendance records for a specific subject.
        /// </summary>
        /// <param name="subjectId">The subject ID.</param>
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetBySubjectId(int subjectId)
        {
            var attendances =
                await _attendanceService.GetBySubjectIdAsync(subjectId);

            return Ok(attendances);
        }

        /// <summary>
        /// Gets attendance records for a specific teacher.
        /// </summary>
        /// <param name="teacherId">The teacher ID.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacherId(int teacherId)
        {
            var attendances =
                await _attendanceService.GetByTeacherIdAsync(teacherId);

            return Ok(attendances);
        }

        /// <summary>
        /// Gets attendance records for a specific date.
        /// </summary>
        /// <param name="date">The attendance date.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetByDate(DateOnly date)
        {
            var attendances =
                await _attendanceService.GetByDateAsync(date);

            return Ok(attendances);
        }

        /// <summary>
        /// Updates an existing attendance record.
        /// </summary>
        /// <param name="id">The attendance record ID.</param>
        /// <param name="dto">The update data for the attendance.</param>
        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(
            int id,
            UpdateAttendanceDto dto)
        {
            var updated = await _attendanceService.UpdateAttendanceAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = "Attendance not found."
                });
            }

            return Ok(new
            {
                Message = "Attendance updated successfully."
            });
        }

        /// <summary>
        /// Deletes an existing attendance record.
        /// </summary>
        /// <param name="id">The attendance record ID.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var deleted =
                await _attendanceService.DeleteAttendanceAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Attendance not found."
                });
            }

            return Ok(new
            {
                Message = "Attendance deleted successfully."
            });
        }
    }
}
