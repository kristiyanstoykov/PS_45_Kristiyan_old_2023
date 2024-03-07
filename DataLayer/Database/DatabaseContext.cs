using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using Welcome.Others;

namespace DataLayer.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFile = "Welcome.db";
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\.."));
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
            modelBuilder.Entity<DatabaseUser>().Property(e => e.Id).ValueGeneratedOnAdd();

            var user = new DatabaseUser
            {
                Id = 1,
                Name = "John Doe",
                Password = "1234",
                Email = "john_doe@email.com",
                FacultyNumber = "",
                Role = UserRolesEnum.ADMIN,
                Expires = DateTime.Now.AddYears(10)
            };

            //Console.WriteLine("Right after John Doe is created before seeding");
            //Console.WriteLine(user.Password);

            var user2 = new DatabaseUser
            {
                Id = 2,
                Name = "user1",
                Password = "pass1",
                Email = "user1@email.com",
                FacultyNumber = "121221010",
                Role = UserRolesEnum.STUDENT,
                Expires = System.DateTime.Now.AddYears(10)
            };
            //Console.WriteLine("Right after user1 is created before seeding");
            //Console.WriteLine(user2.Password);


            modelBuilder.Entity<DatabaseUser>().HasData(user);
            modelBuilder.Entity<DatabaseUser>().HasData(user2);

        }

    }
}
