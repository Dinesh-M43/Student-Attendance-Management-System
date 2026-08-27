using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IAcademicYearRepository
    {
        Task AddAsync(AcademicYear academicYear);

        Task<List<AcademicYear>> GetAllAsync();

        Task<AcademicYear?> GetByIdAsync(int id);

        Task UpdateAsync(AcademicYear academicYear);

        Task DeleteAsync(AcademicYear academicYear);

        Task SaveChangesAsync();
    }
}
