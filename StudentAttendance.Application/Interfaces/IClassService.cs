using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IClassService
    {
        Task<int> CreateClassAsync(CreateClassDto dto);
        Task<List<ClassDto>> GetAllClassesAsync();

        Task<ClassDto?> GetClassByIdAsync(int id);

        Task<bool> UpdateClassAsync(int id, UpdateClassDto dto);

        Task<bool> DeleteClassAsync(int id);
    }
}
