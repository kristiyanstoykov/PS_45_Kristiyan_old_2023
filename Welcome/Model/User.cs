using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;
using BCrypt.Net;

namespace Welcome.Model
{
    public class User
    {
        public string Name { get; set; }

        private string _hashedPassword = string.Empty;
        public string Password
        {
            get { return _hashedPassword; }
            set { _hashedPassword = BCrypt.Net.BCrypt.HashPassword(value); }
        }
        public string Email { get; set; }
        public string FacultyNumber { get; set; }
        public UserRolesEnum Role { get; set; }

        public User() { }

        public User(string name, string pass, string email, string facNum, UserRolesEnum role)
        {
            Name = name;
            Password = pass;
            Email = email;
            FacultyNumber = facNum;
            Role = role;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _hashedPassword);
        }
    }
}
