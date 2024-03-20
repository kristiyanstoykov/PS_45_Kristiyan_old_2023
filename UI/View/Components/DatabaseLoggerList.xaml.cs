using DataLayer.Database;
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

namespace UI.Components
{
    public partial class DatabaseLoggerList : UserControl
    {
        public DatabaseLoggerList()
        {
            InitializeComponent();
        }

        private void Logger_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as DependencyObject;

            while (source != null && !(source is DataGridCell))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            if (source is DataGridCell cell)
            {
                if (cell.Column.Header.ToString() == "TimeStamp")
                {
                    var row = source as DependencyObject;
                    while (row != null && !(row is DataGridRow))
                    {
                        row = VisualTreeHelper.GetParent(row);
                    }

                    if (row is DataGridRow dataGridRow)
                    {
                        var logItem = dataGridRow.Item as DatabaseLogger;
                        if (logItem != null)
                        {
                            string message = $"Time: {logItem.TimeStamp}\nLevel: {logItem.Level}\nMessage: {logItem.Message}";
                            MessageBox.Show(message, "Log Details", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
    }
}
