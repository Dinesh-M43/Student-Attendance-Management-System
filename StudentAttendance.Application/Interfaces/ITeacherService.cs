using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<int> CreateTeacherAsync(CreateTeacherDto dto);

        Task<List<TeacherDto>> GetAllTeachersAsync();

        Task<TeacherDto?> GetTeacherByIdAsync(int id);

        Task<TeacherDto?> GetTeacherByUserIdAsync(string userId);

        Task<bool> UpdateTeacherAsync(int id, UpdateTeacherDto dto);

        Task<bool> DeleteTeacherAsync(int id);
    }
}
