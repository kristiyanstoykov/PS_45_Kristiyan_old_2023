using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.ViewModel;
using Welcome.Model;
using Welcome.Others;

namespace UI.View.Components
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        public event Action<User> UserCreated;
        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var usersPresenter = DataContext as UsersPresenter;

            usersPresenter?.AddUserCommand.Execute(null);
        }
    }
}
