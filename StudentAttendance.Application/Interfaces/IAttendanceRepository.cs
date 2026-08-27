using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        Task AddAsync(Attendance attendance);

        Task<List<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task<Attendance?> GetByStudentSubjectDateAsync(int studentId, int subjectId, DateOnly attendanceDate);

        Task<List<Attendance>> GetByUserIdAsync(string userId);

        Task<List<Attendance>> GetByStudentIdAsync(int studentId);

        Task<List<Attendance>> GetBySubjectIdAsync(int subjectId);

        Task<List<Attendance>> GetByTeacherIdAsync(int teacherId);

        Task<List<Attendance>> GetByDateAsync(DateOnly date);

        Task UpdateAsync(Attendance attendance);

        Task DeleteAsync(Attendance attendance);

        Task SaveChangesAsync();
    }
}
