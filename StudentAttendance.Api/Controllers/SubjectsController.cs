using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// API controller for managing subjects (create, read, update, delete).
    /// </summary>
    /// 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubjectsController"/> class.
        /// </summary>
        /// <param name="subjectService">Service that handles subject operations.</param>

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>
        /// Creates a new subject.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateSubjectDto dto)
        {
            var subjectId =
                await _subjectService.CreateSubjectAsync(dto);

            return CreatedAtAction(
                nameof(GetSubjectById),
                new { id = subjectId },
                new
                {
                    Id = subjectId,
                    Message = "Subject created successfully."
                });
        }

        /// <summary>
        /// Gets all subjects.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects =
                await _subjectService.GetAllSubjectsAsync();

            return Ok(subjects);
        }

        /// <summary>
        /// Gets a subject by ID.
        /// </summary>
        /// <param name="id">The subject ID.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject =
                await _subjectService.GetSubjectByIdAsync(id);

            if (subject == null)
            {
                return NotFound(new
                {
                    Message = "Subject not found."
                });
            }

            return Ok(subject);
        }

        /// <summary>
        /// Updates an existing subject.
        /// </summary>
        /// <param name="id">The subject ID.</param>
        /// /// <param name="dto">The update data for the subject.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(    int id,    UpdateSubjectDto dto)
        {
            var updated = await _subjectService.UpdateSubjectAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = "Subject not found."
                });
            }

            return Ok(new
            {
                Message = "Subject updated successfully."
            });
        }

        /// <summary>
        /// Deletes an existing subject.
        /// </summary>
        /// <param name="id">The subject ID.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var deleted = await _subjectService.DeleteSubjectAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Subject not found."
                });
            }

            return Ok(new
            {
                Message = "Subject deleted successfully."
            });
        }
    }
}
