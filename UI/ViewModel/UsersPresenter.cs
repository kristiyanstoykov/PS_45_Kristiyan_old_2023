using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class UsersPresenter : ObservableObject
    {

        public ObservableCollection<DatabaseUser> Users { get; set; } = new ObservableCollection<DatabaseUser>();

        public UsersPresenter()
        {
            LoadUsers();
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
    }
}
