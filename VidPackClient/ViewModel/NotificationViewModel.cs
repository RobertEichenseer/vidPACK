using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.BL;
using VidPackClient.Common;
using VidPackModel;

namespace VidPackClient.ViewModel
{
    class NotificationViewModel : BindableBase
    {

        private ObservableCollection<NotificationInfo> _notifications;
        public ObservableCollection<NotificationInfo> Notifications
        {
            get { return _notifications; }
            set { _notifications = value; OnPropertyChanged("Notifications"); }
        }

        public ICommonBl Bl { get; set; }
        public NotificationViewModel(ICommonBl bl)
        {
            Bl = bl;
        }

        //Constructor without BL used during Designtime
        public NotificationViewModel()
	    {
            Notifications = new ObservableCollection<NotificationInfo>(); 
	    }

    }
}

