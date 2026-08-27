using StudentAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace StudentAttendance.Application.DTOs
{
    public class RegisterUserDto
    {
        // Common fields
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        // Admin / Student / Teacher
        [Required]
        public string Role { get; set; } = string.Empty;

        // Required for Student and Teacher
        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        // Required only for Student
        public int? ClassId { get; set; }
    }
}
