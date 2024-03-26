using DataLayer.Database;
using DataLayer.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Welcome.Others;

namespace UI.ViewModel
{
    public class AddUserViewModel : ObservableObject
    {

        private string _fullName;
        private string _password;
        private string _facultyNumber;
        private string _email;
        private UserRolesEnum _role;

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                RaisePropertyChangedEvent(nameof(FullName));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChangedEvent(nameof(Password));
            }
        }

        public string FacultyNumber
        {
            get => _facultyNumber;
            set
            {
                _facultyNumber = value;
                RaisePropertyChangedEvent(nameof(FacultyNumber));
            }

        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChangedEvent(nameof(Email));
            }

        }

        public UserRolesEnum Role
        {
            get => _role;
            set
            {
                _role = value;
                RaisePropertyChangedEvent(nameof(Role));
            }

        }

        //private UsersPresenter _usersPresenter;
        //private LoggerPresenter _loggerPresenter;
        private Presenter _presenter;

        //public ObservableCollection<DatabaseUser> Users => _usersPresenter.Users;
        //public ObservableCollection<DatabaseLogger> Logger => _loggerPresenter.Logger;
        public ObservableCollection<DatabaseUser> Users => _presenter.Users;
        public ObservableCollection<DatabaseLogger> Logger => _presenter.Logger;
        public ICommand AddUserCommand
        {
            get
            {
                return new DelegateCommand(AddUser);
            }
        }

        public AddUserViewModel(Presenter presenter)
        {
            _fullName = String.Empty;
            _password = String.Empty;
            _facultyNumber = String.Empty;
            _email = String.Empty;
            _role = UserRolesEnum.ANONYMOUS;

            //_usersPresenter = usersPresenter;
            //_loggerPresenter = loggerPresenter;
            _presenter = presenter;
        }

        //public AddUserViewModel(UsersPresenter usersPresenter, LoggerPresenter loggerPresenter)
        //{
        //    _fullName = String.Empty;
        //    _password = String.Empty;
        //    _facultyNumber = String.Empty;
        //    _email = String.Empty;
        //    _role = UserRolesEnum.ANONYMOUS;

        //    _usersPresenter = usersPresenter;
        //    _loggerPresenter = loggerPresenter;
        //}


        private void AddUser()
        {
            if (Users.Any(u => u.Name.Equals(FullName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("A user with the same name already exists.", "Error Adding User", MessageBoxButton.OK, MessageBoxImage.Error);

                AddLog("ERROR", $"User with name {FullName} already exists.");
            }
            else
            {
                try
                {
                    DatabaseUser FormUser = new DatabaseUser
                    {
                        Name = FullName,
                        Password = Password,
                        FacultyNumber = FacultyNumber,
                        Email = Email,
                        Role = Role,
                        Expires = DateTime.Now.AddYears(1)
                    };

                    Users.Add(FormUser);

                    AddLog("INFO", $"User {FullName} added successfully.");

                    DatabaseService.Add(FormUser);

                    MessageBox.Show($"User {FormUser.Name} successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    FullName = String.Empty;
                    Password = String.Empty;
                    FacultyNumber = String.Empty;
                    Email = String.Empty;
                    Role = UserRolesEnum.ANONYMOUS; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    AddLog("ERROR", $"An error occurred: {ex.Message}!");
                }
            }
        }

        public void AddLog(string level, string message)
        {
            DatabaseLogger logger = new DatabaseLogger()
            {
                TimeStamp = DateTime.Now,
                Level = level,
                Message = message
            };

            Logger.Add(logger);

            DatabaseService.Add(logger);
        }
    }
}
