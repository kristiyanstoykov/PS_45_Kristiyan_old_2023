using DataLayer.Database;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.ViewModel
{
    public class Presenter : ObservableObject
    {
        public string SubjectName { get; set; }
        private ObservableCollection<DatabaseUser> _users = new ObservableCollection<DatabaseUser>();

        public ObservableCollection<DatabaseUser> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    RaisePropertyChangedEvent(nameof(Users)); // Notify the UI of the change
                }
            }
        }

        public ObservableCollection<DatabaseLogger> _logger = new ObservableCollection<DatabaseLogger>();
        public ObservableCollection<DatabaseLogger> Logger
        {
            get => _logger;
            set
            {
                if (_logger != value)
                {
                    _logger = value;
                    RaisePropertyChangedEvent(nameof(_logger)); // Notify the UI of the change
                }
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                return new DelegateCommand(FilterUsersBySubject);
            }
        }

        public Presenter()
        {
            SubjectName = string.Empty;
            LoadUsers();
            LoadLogger();
        }

        private void LoadUsers()
        {
            var records = DatabaseService.GetAllUsers();
            foreach (var user in records)
            {
                Users.Add(user);
            }
        }

        private void ReLoadUsers()
        {
            Users.Clear();
            var records = DatabaseService.GetAllUsers();
            foreach (var user in records)
            {
                Users.Add(user);
            }
        }

        private void LoadLogger()
        {
            var records = DatabaseService.GetAllLogs();
            foreach (var log in records)
            {
                Logger.Add(log);
            }
        }

        public void FilterUsersBySubject()
        {
            if (string.IsNullOrWhiteSpace(SubjectName))
            {
                ReLoadUsers();
                return;
            }

            using var context = new DatabaseContext();
            var filtered = context.Users
                                   .Include(s => s.DatabaseSubjects)
                                   .Where(user => user.DatabaseSubjects.Any(s => s.Name == SubjectName))
                                   .ToList();

            Users.Clear();
            foreach (var user in filtered)
            {
                Users.Add(user);
            }
        }

    }
}
