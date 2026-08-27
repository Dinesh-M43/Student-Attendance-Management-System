using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher);

        Task<List<Teacher>> GetAllAsync();

        Task<Teacher?> GetByIdAsync(int id);

        Task<Teacher?> GetByUserIdAsync(string userId);

        Task<bool> ExistsByEmployeeCodeAsync(
            string employeeCode,
            int? excludeId = null);

        Task<bool> ExistsByEmailAsync(string email);

        Task<string> GetNextEmployeeCodeAsync();

        Task UpdateAsync(Teacher teacher);

        Task DeleteAsync(Teacher teacher);

        Task SaveChangesAsync();
    }
}

