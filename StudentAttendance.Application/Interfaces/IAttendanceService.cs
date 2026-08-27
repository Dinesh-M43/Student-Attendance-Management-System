using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<int> CreateAttendanceAsync(CreateAttendanceDto dto);

        Task<List<AttendanceDto>> GetAllAttendancesAsync();

        Task<AttendanceDto?> GetAttendanceByIdAsync(int id);

        Task<List<AttendanceDto>> GetByStudentIdAsync(int studentId);

        Task<List<AttendanceDto>> GetAttendanceByUserIdAsync(string userId);

        Task<List<AttendanceDto>> GetBySubjectIdAsync(int subjectId);

        Task<List<AttendanceDto>> GetByTeacherIdAsync(int teacherId);

        Task<List<AttendanceDto>> GetByDateAsync(DateOnly date);

        Task<bool> UpdateAttendanceAsync(int id, UpdateAttendanceDto dto);

        Task<bool> DeleteAttendanceAsync(int id);
    }
}
