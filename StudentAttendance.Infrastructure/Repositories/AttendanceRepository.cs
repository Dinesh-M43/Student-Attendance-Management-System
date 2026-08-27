using Microsoft.EntityFrameworkCore;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
        }

        public async Task<List<Attendance>> GetAllAsync()
        {
            return await _context.Attendances.ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Attendance?> GetByStudentSubjectDateAsync(    int studentId,    int subjectId,    DateOnly attendanceDate)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(x =>
                    x.StudentId == studentId &&
                    x.SubjectId == subjectId &&
                    x.AttendanceDate == attendanceDate);
        }

        public async Task<List<Attendance>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Attendances
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetByUserIdAsync(string userId)
        {
            return await _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.Student.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.Attendances
                .Where(x => x.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context.Attendances
                .Where(x => x.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetByDateAsync(DateOnly date)
        {
            return await _context.Attendances
                .Where(x => x.AttendanceDate == date)
                .ToListAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
        }

        public async Task DeleteAsync(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
