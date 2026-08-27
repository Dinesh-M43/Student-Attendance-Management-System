using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Exceptions;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<int> CreateStudentAsync(CreateStudentDto dto)
        {
            var codeExists = await _studentRepository.ExistsByStudentCodeAsync(dto.StudentCode);

            if (codeExists)
            {
                throw new BusinessException( "Student code already exists.");
            }

            var classExists = await _studentRepository.ClassExistsAsync(dto.ClassId);

            if (!classExists)
            {
                throw new BusinessException("Class not found.");
            }

            var student = new Student
            {
                StudentCode = dto.StudentCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                ClassId = dto.ClassId
            };

            await _studentRepository.AddAsync(student);

            await _studentRepository.SaveChangesAsync();

            return student.Id;
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(student => new StudentDto
            {
                Id = student.Id,
                StudentCode = student.StudentCode,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                ClassId = student.ClassId
            }).ToList();
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return null;
            }

            return new StudentDto
            {
                Id = student.Id,
                StudentCode = student.StudentCode,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                ClassId = student.ClassId
            };
        }

        public async Task<StudentDto?> GetStudentByUserIdAsync(string userId)
        {
            var student =
                await _studentRepository.GetByUserIdAsync(userId);

            if (student == null)
            {
                return null;
            }

            return new StudentDto
            {
                Id = student.Id,
                StudentCode = student.StudentCode,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                ClassId = student.ClassId
            };
        }

        public async Task<bool> UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            var student =
                await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return false;
            }

            var codeExists =
                await _studentRepository
                    .ExistsByStudentCodeAsync(dto.StudentCode, id);

            if (codeExists)
            {
                throw new BusinessException(
                    "Student code already exists.");
            }

            var classExists =
                await _studentRepository
                    .ClassExistsAsync(dto.ClassId);

            if (!classExists)
            {
                throw new BusinessException(
                    "Class not found.");
            }

            student.StudentCode = dto.StudentCode;
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.DateOfBirth = dto.DateOfBirth;
            student.Gender = dto.Gender;
            student.ClassId = dto.ClassId;

            await _studentRepository.UpdateAsync(student);

            await _studentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return false;
            }

            await _studentRepository.DeleteAsync(student);



            await _studentRepository.SaveChangesAsync();

            return true;
        }
    }
}
