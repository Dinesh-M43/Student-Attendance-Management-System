using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<int> CreateTeacherAsync(CreateTeacherDto dto)
        {
            var exists =
                await _teacherRepository
                    .ExistsByEmployeeCodeAsync(dto.EmployeeCode);

            if (exists)
            {
                throw new BusinessException(
                    "Employee code already exists.");
            }

            var teacher = new Teacher
            {
                EmployeeCode = dto.EmployeeCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                IsActive = dto.IsActive
            };

            await _teacherRepository.AddAsync(teacher);

            await _teacherRepository.SaveChangesAsync();

            return teacher.Id;
        }

        public async Task<List<TeacherDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();

            return teachers.Select(teacher => new TeacherDto
            {
                Id = teacher.Id,
                EmployeeCode = teacher.EmployeeCode,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                IsActive = teacher.IsActive
            }).ToList();
        }

        public async Task<TeacherDto?> GetTeacherByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return null;
            }

            return new TeacherDto
            {
                Id = teacher.Id,
                EmployeeCode = teacher.EmployeeCode,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                IsActive = teacher.IsActive
            };
        }

        public async Task<TeacherDto?> GetTeacherByUserIdAsync(string userId)
        {
            var teacher =
                await _teacherRepository.GetByUserIdAsync(userId);

            if (teacher == null)
            {
                return null;
            }

            return new TeacherDto
            {
                Id = teacher.Id,
                EmployeeCode = teacher.EmployeeCode,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                DateOfBirth = teacher.DateOfBirth,
                Gender = teacher.Gender
            };
        }

        public async Task<bool> UpdateTeacherAsync(int id, UpdateTeacherDto dto)
        {
            var teacher =
                await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return false;
            }

            var exists =
                await _teacherRepository
                    .ExistsByEmployeeCodeAsync(
                        dto.EmployeeCode,
                        id);

            if (exists)
            {
                throw new BusinessException(
                    "Employee code already exists.");
            }

            teacher.EmployeeCode = dto.EmployeeCode;
            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.Email = dto.Email;
            teacher.Phone = dto.Phone;
            teacher.IsActive = dto.IsActive;

            await _teacherRepository.UpdateAsync(teacher);

            await _teacherRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return false;
            }

            await _teacherRepository.DeleteAsync(teacher);

            await _teacherRepository.SaveChangesAsync();

            return true;
        }
    }
}
