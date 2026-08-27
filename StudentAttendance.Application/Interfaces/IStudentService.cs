using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IStudentService
    {
        Task<int> CreateStudentAsync(CreateStudentDto dto);

        Task<List<StudentDto>> GetAllStudentsAsync();

        Task<StudentDto?> GetStudentByIdAsync(int id);

        Task<StudentDto?> GetStudentByUserIdAsync(string userId);

        Task<bool> UpdateStudentAsync(int id, UpdateStudentDto dto);

        Task<bool> DeleteStudentAsync(int id);
    }
}
