using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Welcome.Model;

namespace DataLayer.Database
{
    public static class DatabaseService
    {
        private static List<T> GetAll<T>() where T : class
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public static void Add<T>(T entity) where T : class
        {
            using (var context = new DatabaseContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }


        public static List<DatabaseUser> GetAllUsers()
        {
            using (var context = new DatabaseContext())
            {
                return context.Users.Include(u => u.DatabaseSubjects).ToList();
            }
        }

        public static List<DatabaseLogger> GetAllLogs()
        {
            return GetAll<DatabaseLogger>();
        }
    }
}
