using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    namespace StudentAttendance.Application.Interfaces
    {
        public interface IClassRepository
        {
            Task AddAsync(Class classEntity);

            Task SaveChangesAsync();

            Task<List<Class>> GetAllAsync();

            Task<Class?> GetByIdAsync(int id);

            Task<bool> AcademicYearExistsAsync(int academicYearId);

            Task UpdateAsync(Class classEntity);

            Task DeleteAsync(Class classEntity);
        }
    }
}
