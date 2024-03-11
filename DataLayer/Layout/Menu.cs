using DataLayer.Database;
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
                Console.WriteLine("4. Validate user");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        Console.Write("Enter name of user to delete: ");
                        string name = Console.ReadLine();
                        if (DeleteUserByName(name))
                        {
                            Console.WriteLine($"User {name} was deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine($"User {name} not found!");
                        }
                        break;
                    case 3:
                        List<DatabaseUser> users = GetAllUsers();
                        Console.WriteLine("All users: ");
                        foreach (var user in users)
                        {
                            Console.WriteLine(" " + UserHelper.ToString(user));
                        }
                        break;
                    case 4:
                        ValidateUser();
                        break;
                    case 5:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;  
                }
                Console.WriteLine("");
            }
        }

        public static List<DatabaseUser> GetAllUsers()
        {
            using (var context = new DatabaseContext())
            {
                context.Add<DatabaseLogger>(new DatabaseLogger
                {
                    TimeStamp = DateTime.Now,
                    Level = "INFO",
                    Message = "Retrieved all users"
                });
                context.SaveChanges();
                return context.Users.ToList();
            }
        }

        public static void ValidateUser()
        {
            Console.WriteLine("Enter user name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            using (var context = new DatabaseContext())
            {
                var users = context.Users.ToList();
                var user = (from u in users
                           where u.Name == name
                           select u)
                           .FirstOrDefault();

                DatabaseLogger dbLogger;

                if (user != null)
                {
                    if (user.VerifyPassword(password))
                    {
                        Console.WriteLine("User is valid");
                        dbLogger = new DatabaseLogger
                        {
                            TimeStamp = DateTime.Now,
                            Level = "INFO",
                            Message = $"User {user.Name} is valid"
                        };
                    }
                    else
                    {
                        Console.WriteLine("Invalid password");
                        dbLogger = new DatabaseLogger
                        {
                            TimeStamp = DateTime.Now,
                            Level = "ERROR",
                            Message = $"Invalid password for user {user.Name}"
                        };
                    }
                }
                else
                {
                    Console.WriteLine("User not found");
                    dbLogger = new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "ERROR",
                        Message = $"User {name} not found"
                    };
                }
            }
        }

        public static void AddUser()
        {
            List<DatabaseUser> users = GetAllUsers();

            String name;
            while (true)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();

                if(users.Any(u => u.Name == name))
                {
                    Console.WriteLine("User with this name already exists. Please enter another name.");
                    continue;
                }
                else
                {
                    break;
                }
            }

            Console.Write("Enter password: ");
            String password = Console.ReadLine();
            Console.Write("Enter faculty number: ");
            String facNum = Console.ReadLine();
            Console.Write("Enter email: ");
            String email = Console.ReadLine();
            Console.Write("Roles: ");
            Console.WriteLine("0. Anonymous, 1. Admin, 2. Inspector, 3. Professor, 4. Student");
            Console.Write("Enter role number: ");
            int roleNum = int.Parse(Console.ReadLine());
            
            var role = roleNum switch
            {
                1 => UserRolesEnum.ADMIN,
                2 => UserRolesEnum.INSPECTOR,
                3 => UserRolesEnum.PROFESSOR,
                4 => UserRolesEnum.STUDENT,
                _ => UserRolesEnum.ANONYMOUS
            };

            DatabaseUser dbUser = new DatabaseUser
            {
                Name = name,
                Password = password,
                Email = email,
                FacultyNumber = facNum,
                Role = role
            };

            DatabaseLogger dbLogger = new DatabaseLogger
            {
                TimeStamp = DateTime.Now,
                Level = "INFO",
                Message = $"Added user {dbUser.Name}"
            };

            using (var context = new DatabaseContext())
            {
                context.Add<DatabaseUser>(dbUser);
                context.Add<DatabaseLogger>(dbLogger);
                context.SaveChanges();
            }

            Console.WriteLine($"User {dbUser.Name} added successfully.");

        }

        public static bool DeleteUserByName(string name)
        {
            using (var context = new DatabaseContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == name);
                if (user != null)
                {
                    context.Remove(user);
                    context.Add<DatabaseLogger>(new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "INFO",
                        Message = $"Deleted user {user.Name}"
                    });

                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
