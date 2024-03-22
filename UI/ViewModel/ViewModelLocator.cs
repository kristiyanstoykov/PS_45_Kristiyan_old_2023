using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class ViewModelLocator
    {
        //public UsersPresenter _usersPresenterVM;
        //public LoggerPresenter _loggerVM;

        public Presenter PresenterVM { get; }
        public AddUserViewModel AddUserVM { get; }

        public ViewModelLocator()
        {
            //_usersPresenterVM = new UsersPresenter();
            //_loggerVM = new LoggerPresenter();

            PresenterVM = new Presenter();
            AddUserVM = new AddUserViewModel(PresenterVM);
        }

        //public UsersPresenter UsersPresenterVM
        //{
        //    get { return _usersPresenterVM; }
        //}

        //public LoggerPresenter LoggerPresenterVM
        //{
        //    get { return _loggerVM; }
        //}

    }
}
