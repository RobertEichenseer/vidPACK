using Apex.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackAdmin.Bl;

namespace VidPackAdmin.ViewModel
{
    class NotificationViewModel : BaseViewModel 
    {
        
        ICommonBl _bl;
        MainAdminViewModel _mainAdminViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bl"> Business Logic</param>
        /// <param name="mainAdminViewModel"></param>
        public NotificationViewModel(ICommonBl bl, MainAdminViewModel mainAdminViewModel)
        {
            _bl = bl;
            _mainAdminViewModel = mainAdminViewModel;
        }

        private NotificationMessageInfo _notificationMessage = new NotificationMessageInfo();
        public NotificationMessageInfo NotificationMessage
        {
            get { return _notificationMessage; }
            set { _notificationMessage = value; OnPropertyChanged("NotificationMessage"); }
        }


        private bool _messageIsEnabled = false;
        public bool MessageIsEnabled
        {
            get { return _messageIsEnabled; }
            set { _messageIsEnabled = value; OnPropertyChanged("MessageIsEnabled"); }
        }

        internal async void SendNotification()
        {
            MainAdminViewModel mainAdminViewModel = ((MainAdminViewModel)App.ViewModel["MainAdminViewModel"]);
            string xmlTemplate = mainAdminViewModel.NotificationType[mainAdminViewModel.NotificationTypeSelectedIndex].XmlTemplate; 
            string notificationTag = mainAdminViewModel.NotificationTag[mainAdminViewModel.NotificationTagSelectedIndex].NotificationTag; 

            //Toast Message
            if (mainAdminViewModel.NotificationTypeSelectedIndex == 0)  
            {
                await _bl.SendToastNotificationAsync(NotificationMessage.Headline, xmlTemplate, notificationTag); 
            }

            //Tile Update
            if (mainAdminViewModel.NotificationTypeSelectedIndex == 1)
            { 
                TileNotification tileNotification = new TileNotification(){
                    Headline = NotificationMessage.Headline, 
                    Line1 = NotificationMessage.Message1,
                    Line2 = NotificationMessage.Message2,
                    Line3 = NotificationMessage.Message3,
                };
                await _bl.SendTileNotificationAsync(tileNotification, xmlTemplate, notificationTag); 
            }
        }
    }
}
