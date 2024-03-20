using DataLayer.Database;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class LoggerPresenter
    {
        public ObservableCollection<DatabaseLogger> Logger { get; set; } = new ObservableCollection<DatabaseLogger>();

        public LoggerPresenter()
        {
            LoadLogger();
        }

        private void LoadLogger()
        {
            using var context = new DatabaseContext();
            var records = context.DatabaseLogger.ToList();
            foreach (var log in records)
            {
                Logger.Add(log);
            }
        }
    }
}
