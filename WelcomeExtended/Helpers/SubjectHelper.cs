using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Welcome.Model;

namespace WelcomeExtended.Helpers
{
    public static class SubjectHelper
    {
        public static string ToString(this Subject subject)
        {
            return $"{subject.Id}. Subject: {subject.Name}";
        }
    }
}
