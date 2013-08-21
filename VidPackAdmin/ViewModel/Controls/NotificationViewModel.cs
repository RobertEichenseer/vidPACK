using Apex.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidPackAdmin.ViewModel
{
    class NotificationViewModel : BaseViewModel 
    {
        private Command _changeView;
        public Command ChangeView
        {
            get { return _changeView;  }
        }

        public NotificationViewModel()
        {
            _changeView = new Command(ShowAdminArea);
        }


        public void ShowAdminArea()
        {
            var x = 1; 
        }
    }
}
