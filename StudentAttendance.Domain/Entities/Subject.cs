using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
