using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class ViewModelLocator
    {
        public UsersPresenter _usersPresenterVM;
        public LoggerPresenter _loggerVM;

        public ViewModelLocator()
        {
            _usersPresenterVM = new UsersPresenter();
            _loggerVM = new LoggerPresenter();
        }

        public UsersPresenter UsersPresenterVM
        {
            get { return _usersPresenterVM ?? (_usersPresenterVM = new UsersPresenter()); }
        }

        public LoggerPresenter LoggerPresenterVM
        {
            get { return _loggerVM ?? (_loggerVM = new LoggerPresenter()); }
        }

    }
}
