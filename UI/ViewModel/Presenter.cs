using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class Presenter : ObservableObject
    {
        public ObservableCollection<DatabaseUser> Users { get; set; } = new ObservableCollection<DatabaseUser>();

        public ObservableCollection<DatabaseLogger> Logger { get; set; } = new ObservableCollection<DatabaseLogger>();

        public Presenter()
        {
            LoadUsers();
            LoadLogger();
        }

        private void LoadUsers()
        {
            using var context = new DatabaseContext();
            var records = context.Users.Include(s => s.DatabaseSubjects).ToList();
            foreach (var user in records)
            {
                Users.Add(user);
            }
        }

        private void LoadLogger()
        {
            using var context = new DatabaseContext();
            var records = context.DatabaseLogger.ToList();
            foreach (var log in records)
            {
                Logger.Add(log);
            }
        }
    }
}
