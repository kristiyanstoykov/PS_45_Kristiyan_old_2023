using Welcome.Others;
using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using static WelcomeExtended.Others.Delegates;

namespace WelcomeExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var user = new User
                {
                    Name = "John Smith",
                    Password = "password123",
                    Email = "email@email.bg",
                    FacultyNumber = "121221010",
                    Role = UserRolesEnum.STUDENT
                };

                var viewModel = new UserViewModel(user);

                var view = new UserView(viewModel);

                view.DisplayUser();

                view.DisplayError();

            }
            catch (Exception e)
            {
                var log = new ActionOnError(Log);
                log(e.Message);

                var fileLog = new ActionOnError(FileLog);
                fileLog(e.Message);
            }
            finally
            {
                Console.WriteLine("Always executed!");
            }

        }
    }
}
