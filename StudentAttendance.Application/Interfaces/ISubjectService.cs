using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<int> CreateSubjectAsync(CreateSubjectDto dto);

        Task<List<SubjectDto>> GetAllSubjectsAsync();

        Task<SubjectDto?> GetSubjectByIdAsync(int id);

        Task<bool> UpdateSubjectAsync(int id, UpdateSubjectDto dto);

        Task<bool> DeleteSubjectAsync(int id);
    }
}
