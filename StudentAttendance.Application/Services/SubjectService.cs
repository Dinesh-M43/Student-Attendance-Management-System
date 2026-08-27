using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<int> CreateSubjectAsync(CreateSubjectDto dto)
        {

            var exists =
                await _subjectRepository.ExistsByCodeAsync(dto.Code);

            if (exists)
            {
                throw new BusinessException(
                    "Subject code already exists.");
            }

            var subject = new Subject
            {
                Code = dto.Code,
                Name = dto.Name,
                IsActive = dto.IsActive
            };

            await _subjectRepository.AddAsync(subject);

            await _subjectRepository.SaveChangesAsync();

            return subject.Id;
        }

        public async Task<List<SubjectDto>> GetAllSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();

            return subjects.Select(subject => new SubjectDto
            {
                Id = subject.Id,
                Code = subject.Code,
                Name = subject.Name,
                IsActive = subject.IsActive
            }).ToList();
        }

        public async Task<SubjectDto?> GetSubjectByIdAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return null;
            }

            return new SubjectDto
            {
                Id = subject.Id,
                Code = subject.Code,
                Name = subject.Name,
                IsActive = subject.IsActive
            };
        }

        public async Task<bool> UpdateSubjectAsync(int id, UpdateSubjectDto dto)
        {
            var subject =
                await _subjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return false;
            }

            var exists =
                await _subjectRepository
                    .ExistsByCodeAsync(dto.Code, id);

            if (exists)
            {
                throw new BusinessException(
                    "Subject code already exists.");
            }

            subject.Code = dto.Code;
            subject.Name = dto.Name;
            subject.IsActive = dto.IsActive;

            await _subjectRepository.UpdateAsync(subject);

            await _subjectRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return false;
            }

            await _subjectRepository.DeleteAsync(subject);

            await _subjectRepository.SaveChangesAsync();

            return true;
        }
    }
}
