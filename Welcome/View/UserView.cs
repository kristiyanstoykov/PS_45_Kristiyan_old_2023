using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.ViewModel;

namespace Welcome.View
{
    internal class UserView
    {
        public UserViewModel _viewModel { get; set; }

        public UserView(UserViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public void DisplayUser()
        {
            Console.WriteLine("User: " + _viewModel._user.Name);
            Console.WriteLine("Role: " + _viewModel._user.Role);
            Console.WriteLine("Email: " + _viewModel._user.Email);
        }

        public void DisplayUserWithFacultyNumber()
        {
            Console.WriteLine("User: " + _viewModel._user.Name);
            Console.WriteLine("Role: " + _viewModel._user.Role);
            Console.WriteLine("Email: " + _viewModel._user.Email);
            Console.WriteLine("Faculty Number: " + _viewModel._user.FacultyNumber);
        }

        public void DisplayUserPassword()
        {
            Console.WriteLine("User: " + _viewModel._user.Name);
            Console.WriteLine("Password: " + _viewModel._user.Password);
        }

    }
}
