using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.Interfaces;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// Controller that exposes endpoints to retrieve attendance reports.
    /// </summary>
    /// 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceReportsController : ControllerBase
    {
        private readonly IAttendanceReportService _reportService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceReportsController"/> class.
        /// </summary>
        /// <param name="reportService">Service used to retrieve attendance reports.</param>

        public AttendanceReportsController(IAttendanceReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Gets the attendance summary for a student.
        /// </summary>
        /// <param name="studentId">
        /// The student ID.
        /// </param>
        /// <param name="fromDate">
        /// Optional start date for the report (inclusive).
        /// </param>
        /// <param name="toDate">
        /// Optional end date for the report (inclusive).
        /// </param>
        
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentReport(
            int studentId,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate)
        {
            if (fromDate.HasValue &&
                toDate.HasValue &&
                fromDate > toDate)
            {
                return BadRequest(new
                {
                    Message = "From date cannot be greater than to date."
                });
            }

            var report =
                await _reportService.GetStudentReportAsync(
                    studentId,
                    fromDate,
                    toDate);

            if (report == null)
            {
                return NotFound(new
                {
                    Message = "Student not found."
                });
            }

            return Ok(report);
        }
    }
}
