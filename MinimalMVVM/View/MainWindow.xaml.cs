using MinimalMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MinimalMVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Presenter _uppercasePresenter;
        private LowercasePresenter _lowercasePresenter;

        public MainWindow()
        {
            InitializeComponent();
            _uppercasePresenter = new Presenter();
            _lowercasePresenter = new LowercasePresenter();
            DataContext = _uppercasePresenter; // Start with uppercase presenter
        }

        private void ToggleConversionType_Checked(object sender, RoutedEventArgs e)
        {
            // Check the toggle state and switch the DataContext accordingly
            if (ToggleConversionType.IsChecked == true)
            {
                DataContext = _lowercasePresenter;
            }
            else
            {
                DataContext = _uppercasePresenter;
            }
        }
    }
}
