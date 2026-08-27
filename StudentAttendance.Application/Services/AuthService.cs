using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using StudentAttendance.Application.DTOs;
using StudentAttendance.Application.Interfaces;
using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;


namespace StudentAttendance.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<AuthResponseDto> RegisterUserAsync(
            RegisterUserDto dto)
        {
            // -----------------------------------------
            // 1. Validate role
            // -----------------------------------------

            var role = dto.Role.Trim();

            if (!role.Equals("Admin", StringComparison.OrdinalIgnoreCase) &&
                !role.Equals("Student", StringComparison.OrdinalIgnoreCase) &&
                !role.Equals("Teacher", StringComparison.OrdinalIgnoreCase))
            {
                throw new Application.Exceptions.BusinessException(
                    "Invalid role. Allowed roles are Admin, Student and Teacher.");
            }

            role = role switch
            {
                var x when x.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                    => "Admin",

                var x when x.Equals("Student", StringComparison.OrdinalIgnoreCase)
                    => "Student",

                var x when x.Equals("Teacher", StringComparison.OrdinalIgnoreCase)
                    => "Teacher",

                _ => role
            };

            // -----------------------------------------
            // 2. Check Identity email
            // -----------------------------------------

            var existingUser =
                await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new Application.Exceptions.BusinessException(
                    "User with this email already exists.");
            }

            // -----------------------------------------
            // 3. Validate Student-specific data
            // -----------------------------------------

            if (role == "Student")
            {
                if (!dto.DateOfBirth.HasValue)
                {
                    throw new Application.Exceptions.BusinessException(
                        "Date of Birth is required for a student.");
                }

                if (string.IsNullOrWhiteSpace(dto.Gender))
                {
                    throw new Application.Exceptions.BusinessException(
                        "Gender is required for a student.");
                }

                if (!dto.ClassId.HasValue)
                {
                    throw new Application.Exceptions.BusinessException(
                        "Class is required for a student.");
                }

                if (await _studentRepository.ExistsByEmailAsync(dto.Email))
                {
                    throw new Application.Exceptions.BusinessException(
                        "A student with this email already exists.");
                }

                if (!await _studentRepository.ClassExistsAsync(dto.ClassId.Value))
                {
                    throw new Application.Exceptions.BusinessException(
                        "Selected class does not exist.");
                }
            }

            // -----------------------------------------
            // 4. Validate Teacher-specific data
            // -----------------------------------------

            if (role == "Teacher")
            {
                if (!dto.DateOfBirth.HasValue)
                {
                    throw new Application.Exceptions.BusinessException(
                        "Date of Birth is required for a teacher.");
                }

                if (string.IsNullOrWhiteSpace(dto.Gender))
                {
                    throw new Application.Exceptions.BusinessException(
                        "Gender is required for a teacher.");
                }

                if (await _teacherRepository.ExistsByEmailAsync(dto.Email))
                {
                    throw new Application.Exceptions.BusinessException(
                        "A teacher with this email already exists.");
                }
            }

            // -----------------------------------------
            // 5. Create Identity user
            // -----------------------------------------

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = $"{dto.FirstName} {dto.LastName}"
            };

            var userResult =
                await _userManager.CreateAsync(
                    user,
                    dto.Password);

            if (!userResult.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    userResult.Errors.Select(x => x.Description));

                throw new Application.Exceptions.BusinessException(
                    errors);
            }

            Student? createdStudent = null;
            Teacher? createdTeacher = null;

            try
            {
                // -----------------------------------------
                // 6. Assign Identity role
                // -----------------------------------------

                var roleResult =
                    await _userManager.AddToRoleAsync(
                        user,
                        role);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        roleResult.Errors.Select(x => x.Description));

                    throw new Application.Exceptions.BusinessException(
                        errors);
                }

                // -----------------------------------------
                // 7. Create Student
                // -----------------------------------------

                if (role == "Student")
                {
                    var studentCode =
                        await _studentRepository
                            .GetNextStudentCodeAsync();

                    createdStudent = new Student
                    {
                        UserId = user.Id,
                        StudentCode = studentCode,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        DateOfBirth = dto.DateOfBirth!.Value,
                        Gender = dto.Gender!,
                        ClassId = dto.ClassId!.Value,
                        IsActive = true
                    };

                    await _studentRepository.AddAsync(
                        createdStudent);

                    await _studentRepository.SaveChangesAsync();
                }

                // -----------------------------------------
                // 8. Create Teacher
                // -----------------------------------------

                if (role == "Teacher")
                {
                    var employeeCode =
                        await _teacherRepository
                            .GetNextEmployeeCodeAsync();

                    createdTeacher = new Teacher
                    {
                        UserId = user.Id,
                        EmployeeCode = employeeCode,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        DateOfBirth = dto.DateOfBirth!.Value,
                        Gender = dto.Gender!,
                        IsActive = true
                    };

                    await _teacherRepository.AddAsync(
                        createdTeacher);

                    await _teacherRepository.SaveChangesAsync();
                }

                // -----------------------------------------
                // 9. Return JWT
                // -----------------------------------------

                return await GenerateTokenAsync(user);
            }
            catch
            {
                // -----------------------------------------
                // Rollback Student
                // -----------------------------------------

                if (createdStudent != null)
                {
                    try
                    {
                        await _studentRepository.DeleteAsync(
                            createdStudent);

                        await _studentRepository.SaveChangesAsync();
                    }
                    catch
                    {
                        // Preserve original exception
                    }
                }

                // -----------------------------------------
                // Rollback Teacher
                // -----------------------------------------

                if (createdTeacher != null)
                {
                    try
                    {
                        await _teacherRepository.DeleteAsync(
                            createdTeacher);

                        await _teacherRepository.SaveChangesAsync();
                    }
                    catch
                    {
                        // Preserve original exception
                    }
                }

                // -----------------------------------------
                // Rollback Identity user
                // -----------------------------------------

                try
                {
                    await _userManager.DeleteAsync(user);
                }
                catch
                {
                    // Preserve original exception
                }

                throw;
            }
        }

        public async Task<AuthResponseDto?> LoginAsync(
            LoginDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return null;
            }

            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!passwordValid)
            {
                return null;
            }

            return await GenerateTokenAsync(user);
        }

        private async Task<AuthResponseDto> GenerateTokenAsync(
            ApplicationUser user)
        {
            var roles =
                await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email ?? string.Empty),

                new Claim(
                    ClaimTypes.Name,
                    user.FullName)
            };

            foreach (var role in roles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role));
            }

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]!));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var expiryMinutes =
                int.Parse(
                    _configuration["Jwt:ExpiryMinutes"]!);

            var expiresAt =
                DateTime.UtcNow.AddMinutes(
                    expiryMinutes);

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims:
                        claims,

                    expires:
                        expiresAt,

                    signingCredentials:
                        credentials);

            return new AuthResponseDto
            {
                Token =
                    new JwtSecurityTokenHandler()
                        .WriteToken(token),

                ExpiresAt =
                    expiresAt,

                UserId =
                    user.Id,

                Email =
                    user.Email ?? string.Empty
            };
        }
    }
}
