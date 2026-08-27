using Microsoft.EntityFrameworkCore;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Repositories
{
    public class AcademicYearRepository : IAcademicYearRepository
    {
        private readonly ApplicationDbContext _context;

        public AcademicYearRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AcademicYear academicYear)
        {
            await _context.AcademicYears.AddAsync(academicYear);
        }

        public async Task<List<AcademicYear>> GetAllAsync()
        {
            return await _context.AcademicYears.ToListAsync();
        }

        public async Task<AcademicYear?> GetByIdAsync(int id)
        {
            return await _context.AcademicYears
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(AcademicYear academicYear)
        {
            _context.AcademicYears.Update(academicYear);
        }

        public async Task DeleteAsync(AcademicYear academicYear)
        {
            _context.AcademicYears.Remove(academicYear);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
