using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;

namespace WelcomeExtended.Data
{
    public class UserData
    {

        private List<User> _users;
        private int _nextId;

        public UserData()
        {
            _users = new List<User>();
            _nextId = 0;
        }

        public void AddUser(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public bool DeleteUser(User user)
        {
            return _users.Remove(user);
        }

        public bool ValidateUser(string name, string password)
        {
            foreach (var user in _users)
            {
                if(user.Name == name && user.VerifyPassword(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateUserLambda(string name, string password)
        {
            return _users.Where(x => x.Name == name && x.VerifyPassword(password))
                .FirstOrDefault() != null ? true : false;
        }

        public bool ValidateUserLambdas(string name, string password)
        {
            var ret = from user in _users
                      where user.Name == name && user.VerifyPassword(password)
                      select user.Id;

            return ret != null ? true : false;
        }

        public User? GetUser(string name, string password)
        {
            return _users.FirstOrDefault(user => user.Name == name && user.VerifyPassword(password)) ?? throw new Exception($"User {name} is not found or wrong password");
        }

        public bool AssignUserRole(string name, UserRolesEnum role)
        {
            var user = _users.FirstOrDefault(x => x.Name == name);
            if (user != null)
            {
                user.Role = role;
                return true;
            }

            return false;
        }

    }
}
