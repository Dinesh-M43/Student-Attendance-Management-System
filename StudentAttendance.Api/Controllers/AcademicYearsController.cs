using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// API controller for managing academicyear (create, read, update, delete).
    /// </summary>
    /// 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearsController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcademicYearsController"/> class.
        /// </summary>
        /// <param name="academicYearService">Service that handles teacher operations.</param>

        public AcademicYearsController(
            IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        /// <summary>
        /// Creates a new academic year.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAcademicYear(
            CreateAcademicYearDto dto)
        {
            var academicYearId =
                await _academicYearService.CreateAcademicYearAsync(dto);

            return CreatedAtAction(
                nameof(GetAcademicYearById),
                new { id = academicYearId },
                new
                {
                    Id = academicYearId,
                    Message = "Academic year created successfully."
                });
        }

        /// <summary>
        /// Gets all academic years.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllAcademicYears()
        {
            var academicYears =
                await _academicYearService
                    .GetAllAcademicYearsAsync();

            return Ok(academicYears);
        }

        /// <summary>
        /// Gets an academic year by ID.
        /// </summary>
        /// <param name="id">The academic year ID.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAcademicYearById(int id)
        {
            var academicYear =
                await _academicYearService
                    .GetAcademicYearByIdAsync(id);

            if (academicYear == null)
            {
                return NotFound(new
                {
                    Message = "Academic year not found."
                });
            }

            return Ok(academicYear);
        }

        /// <summary>
        /// Updates an existing academic year.
        /// </summary>
        /// <param name="id">The academic year ID.</param>
        /// /// <param name="dto">The update data for the academicyear.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcademicYear(
            int id,
            UpdateAcademicYearDto dto)
        {
            var updated =
                await _academicYearService
                    .UpdateAcademicYearAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = "Academic year not found."
                });
            }

            return Ok(new
            {
                Message = "Academic year updated successfully."
            });
        }

        /// <summary>
        /// Deletes an existing academic year.
        /// </summary>
        /// <param name="id">The academic year ID.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicYear(int id)
        {
            var deleted =
                await _academicYearService
                    .DeleteAcademicYearAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Academic year not found."
                });
            }

            return Ok(new
            {
                Message = "Academic year deleted successfully."
            });
        }
    }
}
