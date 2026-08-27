using StudentAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentAttendance.Application.DTOs
{
    public class CreateAttendanceDto
    {
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }

        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }

        [Range(1, int.MaxValue)]
        public int TeacherId { get; set; }

        public DateOnly AttendanceDate { get; set; }

        [EnumDataType(typeof(AttendanceStatus))]
        public AttendanceStatus Status { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }
    }
}
