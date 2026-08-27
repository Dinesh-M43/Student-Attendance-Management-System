using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IAttendanceReportService
    {
        Task<AttendanceReportDto?> GetStudentReportAsync(int studentId, DateOnly? fromDate = null, DateOnly? toDate = null);

    }
}
