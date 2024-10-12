using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using CMCS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CMCS.Data
{
    public class CMCSContext : IdentityDbContext<User>
    {
        public CMCSContext(DbContextOptions<CMCSContext> options) : base(options)
        {
        }

        public DbSet<ClaimForm> Claims { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding roles
            string programmeCoordinatorRoleId = Guid.NewGuid().ToString();
            string academicManagerRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = programmeCoordinatorRoleId,
                    Name = "ProgrammeCoordinator",
                    NormalizedName = "PROGRAMMECOORDINATOR"
                },
                new IdentityRole
                {
                    Id = academicManagerRoleId,
                    Name = "AcademicManager",
                    NormalizedName = "ACADEMICMANAGER"
                }
            );

            // Seeding users
            var hasher = new PasswordHasher<User>();

            // Programme Coordinator user
            string programmeCoordinatorUserId = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = programmeCoordinatorUserId,
                    UserName = "programmecoordinator@cmcs.com",
                    NormalizedUserName = "PROGRAMMECOORDINATOR@CMCS.COM",
                    Email = "programmecoordinator@cmcs.com",
                    NormalizedEmail = "PROGRAMMECOORDINATOR@CMCS.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Coordinator@1"), // Change as needed
                    FullName = "Programme Coordinator"
                }
            );

            // Academic Manager user
            string academicManagerUserId = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = academicManagerUserId,
                    UserName = "academicmanager@cmcs.com",
                    NormalizedUserName = "ACADEMICMANAGER@CMCS.COM",
                    Email = "academicmanager@cmcs.com",
                    NormalizedEmail = "ACADEMICMANAGER@CMCS.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Academic@1"), // Change as needed
                    FullName = "Academic Manager"
                }
            );

            // Assigning roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = programmeCoordinatorRoleId,
                    UserId = programmeCoordinatorUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = academicManagerRoleId,
                    UserId = academicManagerUserId
                }
            );

            // Specify the store type for HourlyRate
            modelBuilder.Entity<ClaimForm>()
                .Property(c => c.HourlyRate)
                .HasColumnType("decimal(10,2)");
        }
    }
}