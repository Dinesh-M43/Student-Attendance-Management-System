using StudentAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public AttendanceStatus Status { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
