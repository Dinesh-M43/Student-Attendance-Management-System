using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentAttendance.Application.DTOs
{
    public class UpdateClassDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Section { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int AcademicYearId { get; set; }
    }
}
