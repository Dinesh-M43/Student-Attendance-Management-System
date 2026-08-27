using Microsoft.EntityFrameworkCore;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Repositories
{ 
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(teacher => teacher.Id == id);
        }

        public async Task<Teacher?> GetByUserIdAsync(string userId)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(teacher => teacher.UserId == userId);
        }

        public async Task<bool> ExistsByEmployeeCodeAsync(
            string employeeCode,
            int? excludeId = null)
        {
            return await _context.Teachers
                .AnyAsync(teacher =>
                    teacher.EmployeeCode == employeeCode &&
                    (!excludeId.HasValue ||
                        teacher.Id != excludeId.Value));
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Teachers
                .AnyAsync(x => x.Email == email);
        }

        public async Task<string> GetNextEmployeeCodeAsync()
        {
            var lastCode = await _context.Teachers
                .Where(x => x.EmployeeCode.StartsWith("EMP"))
                .OrderByDescending(x => x.EmployeeCode)
                .Select(x => x.EmployeeCode)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(lastCode))
            {
                return "EMP0001";
            }

            var number = int.Parse(lastCode.Substring(3));

            return $"EMP{number + 1:D4}";
        }

        public async Task<string> GenerateEmployeeCodeAsync()
        {
            var lastEmployeeCode = await _context.Teachers
                .OrderByDescending(x => x.Id)
                .Select(x => x.EmployeeCode)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(lastEmployeeCode))
            {
                return "EMP0001";
            }

            var numberPart = lastEmployeeCode.Substring(3);

            if (!int.TryParse(numberPart, out int lastNumber))
            {
                return "EMP0001";
            }

            return $"EMP{lastNumber + 1:D4}";
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
        }

        public async Task DeleteAsync(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

