using DataLayer.Database;
using Microsoft.EntityFrameworkCore;
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
    public partial class StudentsList : UserControl
    {
        public StudentsList()
        {
            InitializeComponent();

            using var context = new DatabaseContext();
            var records = context.Users.Include(u => u.DatabaseSubjects).ToList();
            Students.DataContext = records;
        }
    }
}
