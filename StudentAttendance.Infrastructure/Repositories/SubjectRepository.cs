using Microsoft.EntityFrameworkCore;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(subject => subject.Id == id);
        }

        public async Task<bool> ExistsByCodeAsync(    string code,    int? excludeId = null)
        {
            return await _context.Subjects
                .AnyAsync(subject =>
                    subject.Code == code &&
                    (!excludeId.HasValue || subject.Id != excludeId.Value));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
        }

        public async Task DeleteAsync(Subject subject)
        {
            _context.Subjects.Remove(subject);
        }
    }
}
