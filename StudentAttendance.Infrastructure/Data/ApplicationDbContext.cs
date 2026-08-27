using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAttendance.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<AcademicYear> AcademicYears { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Student → ApplicationUser relationship
            modelBuilder.Entity<Student>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasIndex(x => x.UserId)
                .IsUnique();

            // Teacher → ApplicationUser relationship
            modelBuilder.Entity<Teacher>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasIndex(x => x.UserId)
                .IsUnique();

            // StudentCode must be unique
            modelBuilder.Entity<Student>()
                .HasIndex(x => x.StudentCode)
                .IsUnique();

            // EmployeeCode must be unique
            modelBuilder.Entity<Teacher>()
                .HasIndex(x => x.EmployeeCode)
                .IsUnique();

            // One attendance per Student + Subject + Date
            modelBuilder.Entity<Attendance>()
                .HasIndex(x => new
                {
                    x.StudentId,
                    x.SubjectId,
                    x.AttendanceDate
                })
                .IsUnique();
        }
    }
}
