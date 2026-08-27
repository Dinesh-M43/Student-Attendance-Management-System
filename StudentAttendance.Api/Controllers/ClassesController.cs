using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;

namespace StudentAttendance.Api.Controllers
{
    /// <summary>
    /// API controller for managing classes (create, read, update, delete).
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassesController"/> class.
        /// </summary>
        /// <param name="classService">Service that handles class operations.</param>

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        /// <summary>
        /// Creates a new class.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassDto dto)
        {
            var classId = await _classService.CreateClassAsync(dto);

            return CreatedAtAction(
                nameof(GetClassById),
                new { id = classId },
                new
                {
                    Id = classId,
                    Message = "Class created successfully."
                });
        }

        /// <summary>
        /// Gets all classes.
        /// </summary>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes =
                await _classService.GetAllClassesAsync();

            return Ok(classes);
        }

        /// <summary>
        /// Gets a class by ID.
        /// </summary>
        /// <param name="id">The class ID.</param>
        /// 
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classEntity =
                await _classService.GetClassByIdAsync(id);

            if (classEntity == null)
            {
                return NotFound(new
                {
                    Message = "Class not found."
                });
            }

            return Ok(classEntity);
        }

        /// <summary>
        /// Updates an existing class.
        /// </summary>
        /// <param name="id">The class ID.</param>
        ///  /// <param name="dto">The update data for the class.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(    int id,    UpdateClassDto dto)
        {
            var updated = await _classService.UpdateClassAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = "Class not found."
                });
            }

            return Ok(new
            {
                Message = "Class updated successfully."
            });
        }

        /// <summary>
        /// Deletes an existing class.
        /// </summary>
        /// <param name="id">The class ID.</param>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var deleted = await _classService.DeleteClassAsync(id);
            
            if (!deleted)
            {
                return NotFound(new
                {
                    Message = "Class not found."
                });
            }

            return Ok(new
            {
                Message = "Class deleted successfully."
            });
        }
    }
}
