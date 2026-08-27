using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string StudentCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public int ClassId { get; set; }

        public Class Class { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
