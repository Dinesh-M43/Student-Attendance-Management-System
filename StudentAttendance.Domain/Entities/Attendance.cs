using StudentAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public AttendanceStatus Status { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Student Student { get; set; } = null!;

        public Subject Subject { get; set; } = null!;

        public Teacher Teacher { get; set; } = null!;
    }
}
