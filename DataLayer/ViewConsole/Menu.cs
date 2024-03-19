using DataLayer.Database;
using DataLayer.Helpers;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using Welcome.Others;
using WelcomeExtended.Helpers;
using DataLayer.ViewModel;

namespace DataLayer.Layout
{
    public static class Menu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Add user");
                Console.WriteLine("2. Delete user");
                Console.WriteLine("3. Show all users");
                Console.WriteLine("4. Show all subjects");
                Console.WriteLine("5. Validate user");
                Console.WriteLine("6. Add subject");
                Console.WriteLine("7. Add subject to student user");
                Console.WriteLine("8. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        MenuVM.AddUser();
                        break;
                    case 2:
                        Console.Write("Enter name of user to delete: ");
                        string name = Console.ReadLine();
                        if (MenuVM.DeleteUserByName(name))
                        {
                            Console.WriteLine($"User {name} was deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine($"User {name} not found!");
                        }
                        break;
                    case 3:
                        List<DatabaseUser> users = MenuVM.GetAllUsers();
                        Console.WriteLine("All users: ");
                        foreach (var user in users)
                        {
                            Console.WriteLine(" " + DatabaseUserHelper.ToString(user));
                        }
                        break;
                    case 4:
                        List<DatabaseSubject> subjects = MenuVM.GetAllSubjects();
                        Console.WriteLine("All subjects: ");
                        foreach (var subject in subjects)
                        {
                            Console.WriteLine(" " + SubjectHelper.ToString(subject));
                        }
                        break;
                    case 5:
                        MenuVM.ValidateUser();
                        break;
                    case 6:
                        MenuVM.AddSubject();
                        break;
                    case 7:
                        MenuVM.UserAddSubject();
                        break;
                    case 8:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;  
                }
                Console.WriteLine("");
            }
        }

    }
}
