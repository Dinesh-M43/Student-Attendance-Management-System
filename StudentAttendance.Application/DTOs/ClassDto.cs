using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.DTOs
{
    public class ClassDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Section { get; set; } = string.Empty;

        public int AcademicYearId { get; set; }
    }
}
