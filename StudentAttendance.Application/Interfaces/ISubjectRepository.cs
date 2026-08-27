using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject);

        Task<List<Subject>> GetAllAsync();

        Task<Subject?> GetByIdAsync(int id);

        Task<bool> ExistsByCodeAsync(string code, int? excludeId = null);

        Task UpdateAsync(Subject subject);

        Task DeleteAsync(Subject subject);

        Task SaveChangesAsync();
    }
}
