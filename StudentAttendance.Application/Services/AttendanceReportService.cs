using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class AttendanceReportService : IAttendanceReportService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceReportService(
            IStudentRepository studentRepository,
            IAttendanceRepository attendanceRepository)
        {
            _studentRepository = studentRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<AttendanceReportDto?> GetStudentReportAsync(    int studentId,    DateOnly? fromDate = null,    DateOnly? toDate = null)
        {
            // Check whether student exists
            var student =
                await _studentRepository.GetByIdAsync(studentId);

            if (student == null)
            {
                return null;
            }

            // Get all attendance records for the student
            var attendances =
                await _attendanceRepository.GetByStudentIdAsync(studentId);

            // Filter by from date
            if (fromDate.HasValue)
            {
                attendances = attendances
                    .Where(x => x.AttendanceDate >= fromDate.Value)
                    .ToList();
            }

            // Filter by to date
            if (toDate.HasValue)
            {
                attendances = attendances
                    .Where(x => x.AttendanceDate <= toDate.Value)
                    .ToList();
            }

            // Total classes
            var totalClasses = attendances.Count;

            // Count each status
            var present = attendances.Count(x =>
                x.Status == AttendanceStatus.Present);

            var absent = attendances.Count(x =>
                x.Status == AttendanceStatus.Absent);

            var late = attendances.Count(x =>
                x.Status == AttendanceStatus.Late);

            var permission = attendances.Count(x =>
                x.Status == AttendanceStatus.Permission);

            // Calculate percentage
            double attendancePercentage = 0;

            if (totalClasses > 0)
            {
                attendancePercentage =
                    (double)present / totalClasses * 100;
            }

            return new AttendanceReportDto
            {
                StudentId = student.Id,

                StudentName =
                    $"{student.FirstName} {student.LastName}",

                FromDate = fromDate,

                ToDate = toDate,

                TotalClasses = totalClasses,

                Present = present,

                Absent = absent,

                Late = late,

                Permission = permission,

                AttendancePercentage =
                    Math.Round(attendancePercentage, 2)
            };
        }
    }
}
