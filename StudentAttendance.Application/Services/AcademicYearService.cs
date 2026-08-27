using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public AcademicYearService(
            IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<int> CreateAcademicYearAsync(CreateAcademicYearDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
            {
                throw new BusinessException(
                    "Start date must be before end date.");
            }
            var academicYear = new AcademicYear
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            await _academicYearRepository.AddAsync(academicYear);

            await _academicYearRepository.SaveChangesAsync();

            return academicYear.Id;
        }

        public async Task<List<AcademicYearDto>> GetAllAcademicYearsAsync()
        {
            var academicYears =
                await _academicYearRepository.GetAllAsync();

            return academicYears.Select(x => new AcademicYearDto
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<AcademicYearDto?> GetAcademicYearByIdAsync(int id)
        {
            var academicYear =
                await _academicYearRepository.GetByIdAsync(id);

            if (academicYear == null)
            {
                return null;
            }

            return new AcademicYearDto
            {
                Id = academicYear.Id,
                Name = academicYear.Name,
                StartDate = academicYear.StartDate,
                EndDate = academicYear.EndDate,
                IsActive = academicYear.IsActive
            };
        }

        public async Task<bool> UpdateAcademicYearAsync(
            int id,
            UpdateAcademicYearDto dto)
        {
            if (dto.StartDate >= dto.EndDate)
            {
                throw new BusinessException(
                    "Start date must be before end date.");
            }
            var academicYear =
                await _academicYearRepository.GetByIdAsync(id);

            if (academicYear == null)
            {
                return false;
            }

            academicYear.Name = dto.Name;
            academicYear.StartDate = dto.StartDate;
            academicYear.EndDate = dto.EndDate;
            academicYear.IsActive = dto.IsActive;

            await _academicYearRepository.UpdateAsync(academicYear);
            await _academicYearRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAcademicYearAsync(int id)
        {
            var academicYear =
                await _academicYearRepository.GetByIdAsync(id);

            if (academicYear == null)
            {
                return false;
            }

            await _academicYearRepository.DeleteAsync(academicYear);
            await _academicYearRepository.SaveChangesAsync();

            return true;
        }
    }
}
