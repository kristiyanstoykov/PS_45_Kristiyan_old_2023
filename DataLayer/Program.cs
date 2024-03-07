using DataLayer.Model;
using Welcome.Others;
using Welcome.Model;
using WelcomeExtended.Helpers;
using Microsoft.Extensions.Logging;
using DataLayer.Database;
using System.Xml.Linq;
using System.Linq;

namespace DataLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();
                var Stoyo = new DatabaseUser
                {
                    Name = "Stoyo",
                    Password = "Stoyo",
                    Email = "Stoyo@email.com",
                    FacultyNumber = "",
                    Role = UserRolesEnum.ADMIN,
                    Expires = DateTime.Now.AddYears(10)
                };
                Console.WriteLine($"Right after initialization of Stoyo: {Stoyo?.Name} | ID: {Stoyo?.Id} | Hash: {Stoyo?.Password}");
                Console.WriteLine($"Password match: {Stoyo.VerifyPassword("Stoyo")}");
                context.Add(Stoyo);

                context.SaveChanges();

            }

            Console.Write("Write name: ");
            String name = Console.ReadLine();
            Console.Write("Write password: ");
            String password = Console.ReadLine();

            using (var context = new DatabaseContext())
            {
                var user = (from u in context.Users.ToList()
                               where u.Name == name && u.VerifyPassword(password)
                               select u).FirstOrDefault();
                
                if (user != null)
                {
                    Console.WriteLine("Валиден потребител");
                }
                else
                {
                    Console.WriteLine("Невалиден потребител");
                }

            }


        }
    }
}