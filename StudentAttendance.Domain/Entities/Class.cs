using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Domain.Entities
{    public class Class
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Section { get; set; } = string.Empty;

        public int AcademicYearId { get; set; }

        public AcademicYear AcademicYear { get; set; } = null!;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
