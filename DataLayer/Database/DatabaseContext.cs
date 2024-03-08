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
                Name = "admin",
                Password = "admin",
                Email = "admin@email.com",
                FacultyNumber = "",
                Role = UserRolesEnum.ADMIN,
                Expires = DateTime.Now.AddYears(10)
            };


            modelBuilder.Entity<DatabaseUser>().HasData(user);

        }

    }
}
