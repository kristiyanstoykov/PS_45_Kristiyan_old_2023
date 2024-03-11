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

            using (var context = new DatabaseContext())
            {
                var records = context.DatabaseLogger.ToList();
                logger.DataContext = records;
            }
        }

        private void Logger_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the original source of the click
            var source = e.OriginalSource as DependencyObject;

            // Traverse up the visual tree to find the DataGridCell that was clicked
            while (source != null && !(source is DataGridCell))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            // Proceed only if a DataGridCell was found
            if (source is DataGridCell cell)
            {
                // Check if the cell belongs to the "TimeStamp" column
                if (cell.Column.Header.ToString() == "TimeStamp")
                {
                    // Now find the DataGridRow that contains this cell
                    var row = source as DependencyObject;
                    while (row != null && !(row is DataGridRow))
                    {
                        row = VisualTreeHelper.GetParent(row);
                    }

                    // If a row was found, proceed to display the MessageBox
                    if (row is DataGridRow dataGridRow)
                    {
                        // Assuming your log item class has properties named 'Message' and 'Level'
                        var logItem = dataGridRow.Item as DatabaseLogger; // Assuming DatabaseLogger is your model
                        if (logItem != null)
                        {
                            // Format and show the message box
                            string message = $"Time: {logItem.TimeStamp}\nLevel: {logItem.Level}\nMessage: {logItem.Message}";
                            MessageBox.Show(message, "Log Details", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
    }
}
