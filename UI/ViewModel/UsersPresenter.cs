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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.View.Components;
using Welcome.Model;
using Welcome.Others;

namespace UI.ViewModel
{
    public class UsersPresenter : ObservableObject
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string FacultyNumber { get; set; }
        public string Email { get; set; }
        public UserRolesEnum Role { get; set; }


        public ObservableCollection<DatabaseUser> Users { get; set; } = new ObservableCollection<DatabaseUser>();

        public ICommand AddUserCommand => new DelegateCommand(AddUser);

        public UsersPresenter()
        {
            FullName = "";
            Password = "";
            FacultyNumber = "";
            Email = "";
            Role = UserRolesEnum.ANONYMOUS;

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

        private void AddUser()
        {
            if (Users.Any(u => u.Name.Equals(FullName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("A user with the same name already exists.", "Error Adding User", MessageBoxButton.OK, MessageBoxImage.Error);
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

                    using (var context = new DatabaseContext())
                    {
                        context.Add(FormUser);
                        context.SaveChanges();
                    }

                    Users.Add(FormUser);
                    MessageBox.Show($"User {FormUser.Name} successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    FullName = "";
                    Password = "";
                    FacultyNumber = "";
                    Email = "";
                    Role = UserRolesEnum.ANONYMOUS;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
