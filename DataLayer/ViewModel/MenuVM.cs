using DataLayer.Database;
using DataLayer.Helpers;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Others;
using WelcomeExtended.Helpers;

namespace DataLayer.ViewModel
{
    internal class MenuVM
    {
        public static void AddSubject()
        {
            Console.Write("Enter subject name: ");
            string sbName = Console.ReadLine();

            while (string.IsNullOrEmpty(sbName))
            {
                Console.Write("Subject name cannot be empty. Please enter a valid name: ");
                sbName = Console.ReadLine();
            }

            using (var context = new DatabaseContext())
            {
                if (context.Subjects.Any(s => s.Name == sbName))
                {
                    Console.WriteLine("Subject with this name already exists.");
                    context.Add<DatabaseLogger>(new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "Error",
                        Message = $"Subject {sbName} already exists"
                    });
                    return;
                }

                context.Add<DatabaseSubject>(new DatabaseSubject
                {
                    Name = sbName
                });

                context.Add<DatabaseLogger>(new DatabaseLogger
                {
                    TimeStamp = DateTime.Now,
                    Level = "INFO",
                    Message = $"Added subject {sbName}"
                });
                context.SaveChanges();
            }
        }

        public static void UserAddSubject()
        {
            List<DatabaseUser> users = GetAllUsers();
            List<DatabaseUser> students = users.Where(u => u.Role == UserRolesEnum.STUDENT).ToList();
            if (students == null)
            {
                Console.WriteLine("No students in the database!");
                return;
            }

            List<DatabaseSubject> subjects = GetAllSubjects();
            foreach (var student in students)
            {
                Console.WriteLine(DatabaseUserHelper.ToString(student));
            }

            Console.Write("Choose id of student: ");
            int studentId = int.Parse(Console.ReadLine());
            var selectedStudent = students.FirstOrDefault(student => student.Id == studentId);
            while (selectedStudent == null)
            {
                Console.Write("Invalid student id. Please enter a valid id of student: ");
                studentId = int.Parse(Console.ReadLine());
                selectedStudent = students.FirstOrDefault(student => student.Id == studentId);
            }

            Console.WriteLine("All subjects");
            foreach (var subject in subjects)
            {
                Console.WriteLine(" " + SubjectHelper.ToString(subject));
            }

            Console.Write("Choose subject id to add to student: ");
            int subjectId = int.Parse(Console.ReadLine());
            var selectedSubject = subjects.FirstOrDefault(subject => subject.Id == subjectId);
            while (selectedSubject == null)
            {
                Console.Write("Invalid subject id. Please enter a valid id of subject: ");
                subjectId = int.Parse(Console.ReadLine());
                selectedSubject = subjects.FirstOrDefault(subject => subject.Id == subjectId);
            }

            using (var context = new DatabaseContext())
            {
                // Check if the relationship already exists to avoid duplicates
                var existingRelation = context.Set<Dictionary<string, object>>("UserSubject")
                                              .Any(us => (int)us["DatabaseUserId"] == studentId
                                                      && (int)us["DatabaseSubjectId"] == subjectId);

                if (!existingRelation)
                {
                    context.Set<Dictionary<string, object>>("UserSubject").Add(new Dictionary<string, object>
                    {
                        ["DatabaseUserId"] = studentId,
                        ["DatabaseSubjectId"] = subjectId
                    });

                    context.SaveChanges();
                    Console.WriteLine("Subject added to student successfully.");
                    context.Add<DatabaseLogger>(new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "INFO",
                        Message = $"Added subject {selectedSubject.Name} to student {selectedStudent.Name}"
                    });
                }
                else
                {
                    Console.WriteLine("This subject is already associated with the student.");
                    context.Add<DatabaseLogger>(new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "Error",
                        Message = $"Student {selectedStudent.Name} already has subject {selectedSubject.Name}"
                    });
                }
            }
        }

        public static List<DatabaseSubject> GetAllSubjects()
        {
            using (var context = new DatabaseContext())
            {
                context.Add<DatabaseLogger>(new DatabaseLogger
                {
                    TimeStamp = DateTime.Now,
                    Level = "INFO",
                    Message = "Retrieved all subjects"
                });
                context.SaveChanges();
                return context.Subjects.ToList();
            }
        }

        public static List<DatabaseUser> GetAllUsers()
        {
            using (var context = new DatabaseContext())
            {
                // Include the Subjects collection explicitly
                var users = context.Users
                    .Include(u => u.DatabaseSubjects) // Eagerly load DatabaseSubjects
                    .ToList();
                return users;
            }
        }

        public static void ValidateUser()
        {
            Console.Write("Enter user name: ");
            string name = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            using (var context = new DatabaseContext())
            {
                var user = (from u in context.Users
                            where u.Name == name
                            select u).FirstOrDefault();

                DatabaseLogger dbLogger;

                if (user != null)
                {
                    if (user.VerifyPassword(password))
                    {
                        Console.WriteLine("User is valid");
                        context.Add<DatabaseLogger>(new DatabaseLogger
                        {
                            TimeStamp = DateTime.Now,
                            Level = "INFO",
                            Message = $"User {user.Name} is validated"
                        });
                    }
                    else
                    {
                        Console.WriteLine("Invalid password");
                        context.Add<DatabaseLogger>(new DatabaseLogger
                        {
                            TimeStamp = DateTime.Now,
                            Level = "Error",
                            Message = $"Invalid password for {user.Name}"
                        });
                    }
                }
                else
                {
                    Console.WriteLine("User not found");
                    context.Add<DatabaseLogger>(new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "Error",
                        Message = $"User {name} not found"
                    });
                }
                context.SaveChanges();
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

                if (users.Any(u => u.Name == name))
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
                Role = role,
                Expires = DateTime.Now.AddYears(1)
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
                    context.Add<DatabaseLogger>(new DatabaseLogger
                    {
                        TimeStamp = DateTime.Now,
                        Level = "INFO",
                        Message = $"Tried to deleted user {name}"
                    });
                    return false;
                }
            }
        }
    }
}
