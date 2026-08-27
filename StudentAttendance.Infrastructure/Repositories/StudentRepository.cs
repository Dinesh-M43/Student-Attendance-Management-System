using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }


        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .FirstOrDefaultAsync(student => student.Id == id);
        }

        public async Task<Student?> GetByUserIdAsync(string userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(student => student.UserId == userId);
        }

        public async Task<bool> ClassExistsAsync(int classId)
        {
            return await _context.Classes
                .AnyAsync(x => x.Id == classId);
        }

        public async Task<bool> ExistsByStudentCodeAsync(string studentCode, int? excludeId = null)
        {
            return await _context.Students
                .AnyAsync(student =>
                    student.StudentCode == studentCode &&
                    (!excludeId.HasValue ||
                     student.Id != excludeId.Value));
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Students
                .AnyAsync(x => x.Email == email);
        }

        public async Task<string> GetNextStudentCodeAsync()
        {
            var lastCode = await _context.Students
                .Where(x => x.StudentCode.StartsWith("STU"))
                .OrderByDescending(x => x.StudentCode)
                .Select(x => x.StudentCode)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(lastCode))
            {
                return "STU0001";
            }

            var number = int.Parse(lastCode.Substring(3));

            return $"STU{number + 1:D4}";
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
        }        

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
