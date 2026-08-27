using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using StudentAttendance.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AttendanceService(
            IAttendanceRepository attendanceRepository,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<int> CreateAttendanceAsync(
            CreateAttendanceDto dto)
        {
            // Check if Student exists
            var student =
                await _studentRepository.GetByIdAsync(dto.StudentId);

            if (student == null)
            {
                throw new BusinessException("Student not found.");
            }



            // Check if Subject exists
            var subject =
                await _subjectRepository.GetByIdAsync(dto.SubjectId);

            if (subject == null)
            {
                throw new BusinessException("Subject not found.");
            }

            // Check if Teacher exists
            var teacher =
                await _teacherRepository.GetByIdAsync(dto.TeacherId);

            if (teacher == null)
            {
                throw new BusinessException("Teacher not found.");
            }

            // Check duplicate attendance
            var existingAttendance =
                await _attendanceRepository.GetByStudentSubjectDateAsync(
                    dto.StudentId,
                    dto.SubjectId,
                    dto.AttendanceDate);

            if (existingAttendance != null)
            {
                throw new BusinessException(
                    "Attendance already exists for this student, subject and date.");
            }

            // Create attendance
            var attendance = new Attendance
            {
                StudentId = dto.StudentId,
                SubjectId = dto.SubjectId,
                TeacherId = dto.TeacherId,
                AttendanceDate = dto.AttendanceDate,
                Status = dto.Status,
                Remarks = dto.Remarks
            };

            await _attendanceRepository.AddAsync(attendance);

            await _attendanceRepository.SaveChangesAsync();

            return attendance.Id;
        }

        public async Task<List<AttendanceDto>> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAllAsync();

            return attendances.Select(x => new AttendanceDto
            {
                Id = x.Id,
                StudentId = x.StudentId,
                SubjectId = x.SubjectId,
                TeacherId = x.TeacherId,
                AttendanceDate = x.AttendanceDate,
                Status = x.Status,
                Remarks = x.Remarks,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
        }

        public async Task<AttendanceDto?> GetAttendanceByIdAsync(int id)
        {
            var attendance =
                await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
            {
                return null;
            }

            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                SubjectId = attendance.SubjectId,
                TeacherId = attendance.TeacherId,
                AttendanceDate = attendance.AttendanceDate,
                Status = attendance.Status,
                Remarks = attendance.Remarks,
                CreatedAt = attendance.CreatedAt,
                UpdatedAt = attendance.UpdatedAt
            };
        }

        public async Task<List<AttendanceDto>> GetAttendanceByUserIdAsync(
    string userId)
        {
            var attendances =
                await _attendanceRepository.GetByUserIdAsync(userId);

            return attendances.Select(a => new AttendanceDto
            {
                Id = a.Id,
                StudentId = a.StudentId,
                SubjectId = a.SubjectId,
                TeacherId = a.TeacherId,
                AttendanceDate = a.AttendanceDate,
                Status = a.Status
            }).ToList();
        }

        private AttendanceDto MapToDto(Attendance attendance)
        {
            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                SubjectId = attendance.SubjectId,
                TeacherId = attendance.TeacherId,
                AttendanceDate = attendance.AttendanceDate,
                Status = attendance.Status,
                Remarks = attendance.Remarks,
                CreatedAt = attendance.CreatedAt,
                UpdatedAt = attendance.UpdatedAt
            };
        }

        public async Task<List<AttendanceDto>> GetByStudentIdAsync(int studentId)
        {
            var attendances =
                await _attendanceRepository.GetByStudentIdAsync(studentId);

            return attendances
                .Select(MapToDto)
                .ToList();
        }

        public async Task<List<AttendanceDto>> GetBySubjectIdAsync(int subjectId)
        {
            var attendances =
                await _attendanceRepository.GetBySubjectIdAsync(subjectId);

            return attendances
                .Select(MapToDto)
                .ToList();
        }

        public async Task<List<AttendanceDto>> GetByTeacherIdAsync(int teacherId)
        {
            var attendances =
                await _attendanceRepository.GetByTeacherIdAsync(teacherId);

            return attendances
                .Select(MapToDto)
                .ToList();
        }

        public async Task<List<AttendanceDto>> GetByDateAsync(DateOnly date)
        {
            var attendances =
                await _attendanceRepository.GetByDateAsync(date);

            return attendances
                .Select(MapToDto)
                .ToList();
        }

        public async Task<bool> UpdateAttendanceAsync(int id, UpdateAttendanceDto dto)
        {
            var attendance =
                await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
            {
                return false;
            }

            // Check duplicate attendance
            var existingAttendance =
                await _attendanceRepository.GetByStudentSubjectDateAsync(
                    dto.StudentId,
                    dto.SubjectId,
                    dto.AttendanceDate);

            if (existingAttendance != null &&
                existingAttendance.Id != id)
            {
                throw new BusinessException(
                    "Attendance already exists for this student, subject and date.");
            }

            var student =
                await _studentRepository.GetByIdAsync(dto.StudentId);

            if (student == null)
            {
                throw new BusinessException("Student not found.");
            }

            var subject =
                await _subjectRepository.GetByIdAsync(dto.SubjectId);

            if (subject == null)
            {
                throw new BusinessException("Subject not found.");
            }

            var teacher =
                await _teacherRepository.GetByIdAsync(dto.TeacherId);

            if (teacher == null)
            {
                throw new BusinessException("Teacher not found.");
            }

            attendance.StudentId = dto.StudentId;
            attendance.SubjectId = dto.SubjectId;
            attendance.TeacherId = dto.TeacherId;
            attendance.AttendanceDate = dto.AttendanceDate;
            attendance.Status = dto.Status;
            attendance.Remarks = dto.Remarks;
            attendance.UpdatedAt = DateTime.UtcNow;

            await _attendanceRepository.UpdateAsync(attendance);

            await _attendanceRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAttendanceAsync(int id)
        {
            var attendance =
                await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
            {
                return false;
            }

            await _attendanceRepository.DeleteAsync(attendance);

            await _attendanceRepository.SaveChangesAsync();

            return true;
        }
    }
}
