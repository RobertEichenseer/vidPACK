using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VidPackAdmin.Bl;
using VidPackAdmin.View.Controls;
using VidPackModel;

namespace VidPackAdmin.ViewModel
{
    internal class MainAdminViewModel : BaseViewModel
    {
        /// <summary>
        /// General
        /// </summary>
        private ICommonBl _bl { get; set; }
       
        private int _selectedAdminAreaIndex;
        public int SelectedAdminAreaIndex
        {
            get { return _selectedAdminAreaIndex; }
            set { _selectedAdminAreaIndex = value; OnPropertyChanged("SelectedAdminAreaIndex"); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bl"></param>
        public MainAdminViewModel(ICommonBl bl)
        {
            _bl = bl;
            InitNotificationType();
            InitNotificationTag();
            LoadExistingNotificationTag(); 
        }

        

        /// <summary>
        /// View Model Data - Notifification
        /// </summary>
        private ObservableCollection<NotificationTemplate> _notificationType = new ObservableCollection<NotificationTemplate>(); 
        public ObservableCollection<NotificationTemplate> NotificationType
        {
            get { return _notificationType; }
            set { _notificationType = value; OnPropertyChanged("NotificationType"); }
        }

        private ObservableCollection<NotificationInfo> _notificationTag = new ObservableCollection<NotificationInfo>();
        public ObservableCollection<NotificationInfo> NotificationTag
        {
            get { return _notificationTag; }
            set {_notificationTag = value; OnPropertyChanged("NotificationTag");}
        }


        private void InitNotificationType()
        {
            NotificationType.Add(new NotificationTemplate()
            {
                Name = "Toast",
                XmlTemplate = "<toast><visual><binding template=\"ToastText01\"><text id=\"1\">{0}</text></binding></visual></toast>",
            });
            NotificationType.Add(new NotificationTemplate()
            {
                Name = "TileUpdate",
                XmlTemplate = "<tile><visual><binding template=\"TileSquareText01\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text></binding></visual></tile>",
            });
        }

        private void InitNotificationTag()
        {
            NotificationTag.Add(new NotificationInfo() { NotificationTag = "...  Loading  ..." });
        }


        /// <summary>
        /// BL Calls
        /// </summary>
        private async void LoadExistingNotificationTag()
        {
            NotificationTag = new ObservableCollection<NotificationInfo>(await _bl.LoadNotificationTagAsync()); 
        }


        internal Control GetAdminAreaControl()
        {
            switch (SelectedAdminAreaIndex)
            {
                case 0:
                {
                    return new Notification();
                }
                case 1:
                case 2:
                {
                    //not yet implemented
                    break;
                }

                case 3:
                {
                    return new Setting(_bl);
                }
            }
            return null; 
        }
    }

}
