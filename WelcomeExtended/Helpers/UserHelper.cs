using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using WelcomeExtended.Data;

namespace WelcomeExtended.Helpers
{
    public static class UserHelper
    {
        public static string ToString(this User user)
        {
            return $"{user.Id}. Name: {user.Name}, Email: {user.Email}, Faculty Number: {user.FacultyNumber}, Role: {user.Role}";
        }

    }
}
