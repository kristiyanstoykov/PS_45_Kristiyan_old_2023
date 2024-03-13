using Welcome.Others;
using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using WelcomeExtended.Data;
using static WelcomeExtended.Others.Delegates;
using static WelcomeExtended.Helpers.UserHelper;
using WelcomeExtended.Helpers;

namespace WelcomeExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
            //    var user = new User
            //    {
            //        Name = "John Smith",
            //        Password = "password123",
            //        Email = "email@email.bg",
            //        FacultyNumber = "121221010",
            //        Role = UserRolesEnum.STUDENT
            //    };

            //    var viewModel = new UserViewModel(user);

            //    var view = new UserView(viewModel);

            //    view.DisplayUser();

            //    view.DisplayError();

            //}
            //catch (Exception e)
            //{
            //    var log = new ActionOnError(Log);
            //    log(e.Message);

            //    var fileLog = new ActionOnError(FileLog);
            //    fileLog(e.Message);
            //}
            //finally
            //{
            //    Console.WriteLine("Always executed!");
            //}

            UserData userData = new UserData();

            User studentUser = new User()
            {
                Name = "John Smith",
                Password = "password123",
                Email = "email@email.bg",
                FacultyNumber = "121221010",
                Role = UserRolesEnum.STUDENT
            };

            userData.AddUser(studentUser);

            User studentUser2 = new User()
            {
                Name = "Student2",
                Password = "123",
                Email = "email@email.bg",
                FacultyNumber = "121221221",
                Role = UserRolesEnum.STUDENT
            };

            User teacherUser = new User()
            {
                Name = "Teacher",
                Password = "1234",
                Email = "email@email.bg",
                FacultyNumber = "121223344",
                Role = UserRolesEnum.PROFESSOR
            };

            User adminUser = new User()
            {
                Name = "Admin",
                Password = "12345",
                Email = "email@email.bg",
                FacultyNumber = "",
                Role = UserRolesEnum.ADMIN
            };

            userData.AddUser(studentUser2);
            userData.AddUser(teacherUser);
            userData.AddUser(adminUser);

            Console.Write("Write name: ");
            String name = Console.ReadLine();
            Console.Write("Write password: ");
            String password = Console.ReadLine();

            bool userExists = userData.ValidateUser(name, password);
            var logError = new ActionOnError(UserLogError);
            var logSuccess = new ActionOnSuccess(UserLogSuccess);

            User user = new User();

            try
            {
                user = userData.GetUser(name, password);
                Console.WriteLine(UserHelper.ToString(user));
                logSuccess($"User {name} successfully logged!");
            }
            catch (Exception e)
            {
                logError(e.Message);
            }

        }
    }
}
