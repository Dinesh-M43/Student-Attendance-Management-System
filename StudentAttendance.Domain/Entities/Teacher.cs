using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
