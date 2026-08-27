using StudentAttendance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Interfaces
{
    public interface IAcademicYearService
    {
        Task<int> CreateAcademicYearAsync(CreateAcademicYearDto dto);

        Task<List<AcademicYearDto>> GetAllAcademicYearsAsync();

        Task<AcademicYearDto?> GetAcademicYearByIdAsync(int id);

        Task<bool> UpdateAcademicYearAsync(            int id,            UpdateAcademicYearDto dto);

        Task<bool> DeleteAcademicYearAsync(int id);
    }
}
