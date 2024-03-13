using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;

namespace DataLayer.Helpers
{
    public static class DatabaseUserHelper
    {
        public static string ToString(this DatabaseUser user)
        {
            var subjectsList = user.DatabaseSubjects?.Select(s => s.Name) ?? Enumerable.Empty<string>();
            var subjectsString = string.Join(", ", subjectsList);

            return $"{user.Id}. Name: {user.Name}, Email: {user.Email}, Faculty Number: {user.FacultyNumber}, Role: {user.Role}, Subjects: [{subjectsString}]";
        }
    }
}
