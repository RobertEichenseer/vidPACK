using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl;
using VidPackClient.Common;
using VidPackModel;
using Windows.Storage;

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

        public ICommonBl _Bl { get; set; }
        public NotificationViewModel(ICommonBl bl)
        {
            _Bl = bl;
        }

        //Constructor without BL used during Designtime
        public NotificationViewModel()
	    {
            List<NotificationInfo> notifications = new List<NotificationInfo>();

            notifications.Add(new NotificationInfo() { NotificationTag = "UG Alert", IsSubscribed=true, });
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 1", IsSubscribed = true, });
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 2", IsSubscribed = false, });
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 3", IsSubscribed = true, });
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 4", IsSubscribed = false });
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 5", IsSubscribed = true });
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 6", IsSubscribed = true,});
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 7", IsSubscribed = false,});
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 8", IsSubscribed = false,});
            notifications.Add(new NotificationInfo() { NotificationTag = "Notification Tag 9", IsSubscribed = true,});

            Notifications = new ObservableCollection<NotificationInfo>(notifications); 
	    }


        internal async void LoadNotifications()
        {
            //Load existing notifications from BE
            Notifications = new ObservableCollection<NotificationInfo>(await _Bl.LoadNotifications()); 

            //Load local subscribed notifications
            List<string> localSubscribedNotification = LoadSubscribedNotificationsLocal();
            MergeSubscribedNotifications(localSubscribedNotification);
            SaveSubscribedNotifications(); 

        }

        private void MergeSubscribedNotifications(List<string> localSubscribedNotification)
        {
            foreach (NotificationInfo notificationInfo in Notifications)
            {
                if (localSubscribedNotification.Contains(notificationInfo.NotificationTag))
                    notificationInfo.IsSubscribed = true;
            }
        }

        internal void ProcessNotificationChange()
        {
            SaveSubscribedNotifications();
        }

        public List<string> LoadSubscribedNotificationsLocal()
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            if (!applicationDataContainer.Values.ContainsKey("subscribedNotification"))
            {
                applicationDataContainer.Values.Add("subscribedNotification", "");
                return new List<string>(); 
            }

            return (applicationDataContainer.Values["subscribedNotification"] as string).Split('\n').ToList<string>();
        }

        private async void SaveSubscribedNotifications()
        {
            List<string> notification = Notifications.Where(item => item.IsSubscribed == true).Select(item => item.NotificationTag).ToList<string>(); 
            string subscribedNotification = String.Join("\n", notification);

            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            applicationDataContainer.Values["subscribedNotification"] = subscribedNotification;

            await App._NotificationHub.RegisterNativeAsync(App._ChannelUri, notification); 
        }
    }
}

