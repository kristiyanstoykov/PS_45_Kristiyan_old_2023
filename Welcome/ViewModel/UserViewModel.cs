using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;

namespace Welcome.ViewModel
{
    public class UserViewModel
    {
        public User _user { get; set; }

        public string Name
        {
            get { return _user.Name; }
            set { _user.Name = value; }
        }

        public string Password
        {
            get { return _user.Password; }
            set { _user.Password = value; }
        }

        public string Email
        {
            get { return _user.Email; }
            set { _user.Email = value; }
        }

        public string FacultyNumber
        {
            get { return _user.FacultyNumber; }
            set { _user.FacultyNumber = value; }
        }

        public UserRolesEnum Roles
        {
            get { return _user.Role; }
            set { _user.Role = value; }
        }

        public UserViewModel(User user)
        {
            this._user = user;
        }

    }
}
