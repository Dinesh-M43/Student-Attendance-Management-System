using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StudentAttendance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Infrastructure.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(
            IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
                "Admin",
                "Teacher",
                "Student"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUserRolesAsync(
            IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser =
                await userManager.FindByEmailAsync(
                    "admin@studentattendance.com");

            if (adminUser != null &&
                !await userManager.IsInRoleAsync(
                    adminUser,
                    "Admin"))
            {
                await userManager.AddToRoleAsync(
                    adminUser,
                    "Admin");
            }

            var studentUser =
                await userManager.FindByEmailAsync(
                    "student@studentattendance.com");

            if (studentUser != null &&
                !await userManager.IsInRoleAsync(
                    studentUser,
                    "Student"))
            {
                await userManager.AddToRoleAsync(
                    studentUser,
                    "Student");
            }
        }
    }
}
