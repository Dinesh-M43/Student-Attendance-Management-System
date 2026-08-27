using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace StudentAttendance.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student);

        Task<List<Student>> GetAllAsync();

        Task<Student?> GetByIdAsync(int id);

        Task<Student?> GetByUserIdAsync(string userId);

        Task<bool> ClassExistsAsync(int classId);

        Task<bool> ExistsByStudentCodeAsync(
            string studentCode,
            int? excludeId = null);

        Task<bool> ExistsByEmailAsync(string email);

        Task<string> GetNextStudentCodeAsync();

        Task UpdateAsync(Student student);

        Task DeleteAsync(Student student);

        Task SaveChangesAsync();
    }
}
