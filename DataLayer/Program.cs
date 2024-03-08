using DataLayer.Model;
using Welcome.Others;
using Welcome.Model;
using WelcomeExtended.Helpers;
using Microsoft.Extensions.Logging;
using DataLayer.Database;
using System.Xml.Linq;
using System.Linq;
using DataLayer.Layout;

namespace DataLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();
            }

            Menu.ShowMenu();


        }
    }
}