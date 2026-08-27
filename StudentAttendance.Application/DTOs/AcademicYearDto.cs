using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.DTOs
{    public class AcademicYearDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
