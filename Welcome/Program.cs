using Welcome.Model;

namespace Welcome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("asdasd");
            //User user = new User("Kris", "1234", "email@tu-sofia.bg", "121221010", Others.UserRolesEnum.STUDENT);
            //ViewModel.UserViewModel viewModel = new ViewModel.UserViewModel(user);
            //View.UserView view = new View.UserView(viewModel);

            //view.DisplayUser();
            //view.DisplayUserPassword();
            //Console.WriteLine(user.VerifyPassword("1234"));

            var knownHash = "$2a$11$O8Qw2sHhHguYt051aEcAaeBBhCcAqCMGSs/TEFwqykANEPHpU7WHK";
            var plainPassword = "Stoyo";
            var verificationResult = BCrypt.Net.BCrypt.Verify(plainPassword, knownHash);
            Console.WriteLine($"Direct verification result: {verificationResult}");
        }
    }
}
