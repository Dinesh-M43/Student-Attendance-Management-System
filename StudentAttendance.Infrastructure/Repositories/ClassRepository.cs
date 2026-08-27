using Microsoft.EntityFrameworkCore;
using StudentAttendance.Application.Interfaces.StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Class classEntity)
        {
            await _context.Classes.AddAsync(classEntity);
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AcademicYearExistsAsync(int academicYearId)
        {
            return await _context.AcademicYears
                .AnyAsync(x => x.Id == academicYearId);
        }

        public async Task UpdateAsync(Class classEntity)
        {
            _context.Classes.Update(classEntity);
        }

        public async Task DeleteAsync(Class classEntity)
        {
            _context.Classes.Remove(classEntity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
