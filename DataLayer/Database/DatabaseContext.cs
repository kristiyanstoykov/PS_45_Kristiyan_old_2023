using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using Welcome.Others;
using Welcome.Model;

namespace DataLayer.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; } = null!;
        public DbSet<DatabaseSubject> Subjects { get; set; } = null!;
        public DbSet<DatabaseLogger> DatabaseLogger { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFile = "Welcome.db";
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\.."));
            string databaseDir = Path.Combine(projectRoot, "db");

            if (!Directory.Exists(databaseDir))
            {
                Directory.CreateDirectory(databaseDir);
            }

            string databasePath = databaseDir + $"\\{databaseFile}";

            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseUser>()
                .HasMany(u => u.DatabaseSubjects)
                .WithMany(s => s.DatabaseUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSubject",
                    j => j.HasOne<DatabaseSubject>()
                          .WithMany()
                          .HasForeignKey("DatabaseSubjectId")
                          .OnDelete(DeleteBehavior.Cascade), // Adjust the delete behavior as needed
                    j => j.HasOne<DatabaseUser>()
                          .WithMany()
                          .HasForeignKey("DatabaseUserId")
                          .OnDelete(DeleteBehavior.Cascade), // Adjust the delete behavior as needed
                    j =>
                    {
                        j.ToTable("UserSubjects"); // The name of the join table
                        // Optionally, specify the primary key of the join table if needed
                        j.HasKey("DatabaseUserId", "DatabaseSubjectId");
                    });

            modelBuilder.Entity<DatabaseUser>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DatabaseSubject>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DatabaseLogger>().Property(e => e.Id).ValueGeneratedOnAdd();

            var user = new DatabaseUser
            {
                Id = 1,
                Name = "admin",
                Password = "admin",
                Email = "admin@email.com",
                FacultyNumber = "",
                Role = UserRolesEnum.ADMIN,
                Expires = DateTime.Now.AddYears(10),
                DatabaseSubjects = new List<DatabaseSubject>()
            };

            var subject = new DatabaseSubject
            {
                Id = 1,
                Name = "English"
            };

            var logger = new DatabaseLogger
            {
                Id = 1,
                TimeStamp = DateTime.Now,
                Level = "INFO",
                Message = "Database initialized"
            };

            // Seed entities
            modelBuilder.Entity<DatabaseUser>().HasData(user);
            modelBuilder.Entity<DatabaseSubject>().HasData(subject);
            modelBuilder.Entity("UserSubject")
                .HasData(
                    new { DatabaseUserId = 1, DatabaseSubjectId = 1 }
                );
            modelBuilder.Entity<DatabaseLogger>().HasData(logger);

        }

    }
}
