using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Application.Interfaces.StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<int> CreateClassAsync(CreateClassDto dto)
        {
            var academicYearExists = await _classRepository.AcademicYearExistsAsync(dto.AcademicYearId);

            if (!academicYearExists)
            {
                throw new BusinessException(
                    "Academic year not found.");
            }
            var classEntity = new Class
            {
                Name = dto.Name,
                Section = dto.Section,
                AcademicYearId = dto.AcademicYearId
            };

            await _classRepository.AddAsync(classEntity);

            await _classRepository.SaveChangesAsync();

            return classEntity.Id;
        }

        public async Task<List<ClassDto>> GetAllClassesAsync()
        {
            var classes = await _classRepository.GetAllAsync();

            return classes.Select(x => new ClassDto
            {
                Id = x.Id,
                Name = x.Name,
                Section = x.Section,
                AcademicYearId = x.AcademicYearId
            }).ToList();
        }

        public async Task<ClassDto?> GetClassByIdAsync(int id)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);

            if (classEntity == null)
            {
                return null;
            }

            return new ClassDto
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                Section = classEntity.Section,
                AcademicYearId = classEntity.AcademicYearId
            };
        }

        public async Task<bool> UpdateClassAsync(    int id,    UpdateClassDto dto)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);

            if (classEntity == null)
            {
                return false;
            }

            var academicYearExists = await _classRepository.AcademicYearExistsAsync(dto.AcademicYearId);

            if (!academicYearExists)
            {
                throw new BusinessException("Academic year not found.");
            }

            classEntity.Name = dto.Name;
            classEntity.Section = dto.Section;
            classEntity.AcademicYearId = dto.AcademicYearId;

            await _classRepository.UpdateAsync(classEntity);

            await _classRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);

            if (classEntity == null)
            {
                return false;
            }

            await _classRepository.DeleteAsync(classEntity);

            await _classRepository.SaveChangesAsync();

            return true;
        }
    }
}
