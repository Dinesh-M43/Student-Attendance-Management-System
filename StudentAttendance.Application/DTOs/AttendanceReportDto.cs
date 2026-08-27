using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.DTOs
{
    public class AttendanceReportDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public int TotalClasses { get; set; }

        public int Present { get; set; }

        public int Absent { get; set; }

        public int Late { get; set; }

        public int Permission { get; set; }

        public double AttendancePercentage { get; set; }
    }
}
