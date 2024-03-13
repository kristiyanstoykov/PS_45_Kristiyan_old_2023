using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;
using BCrypt.Net;
using System.ComponentModel;

namespace Welcome.Model
{
    public class User
    {
        public virtual int Id { get; set; }

        public string Name { get; set; }

        private string _hashedPassword = string.Empty;

        public string Password
        {
            get { return _hashedPassword; }
            set
            {
                if (!IsHashed(value))
                {
                    _hashedPassword = BCrypt.Net.BCrypt.HashPassword(value);
                }
                else
                {
                    _hashedPassword = value;
                }
            }
        }

        public string Email { get; set; }
        public string FacultyNumber { get; set; }
        public UserRolesEnum Role { get; set; }

        public DateTime? Expires { get; set; }

        public User() { }

        public User(string name, string pass, string email, string facNum, UserRolesEnum role)
        {
            Name = name;
            Password = pass;
            Email = email;
            FacultyNumber = facNum;
            Role = role;
        }

        public void SetActive(DateTime validDate)
        {
            Expires = validDate;
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _hashedPassword);
        }

        private bool IsHashed(string value)
        {
            return value != null && System.Text.RegularExpressions.Regex.IsMatch(value, @"^\$2[aby]\$\d{2}\$[./A-Za-z0-9]{53}$");
        }
    }
}
