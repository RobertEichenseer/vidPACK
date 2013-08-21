using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackAdmin.Bl;
using VidPackModel;

namespace VidPackAdmin.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommonBl Bl {get; set;}

        private int _notificationTemplateSelectedIndex = 0;
        public int NotificationTemplateSelectedIndex
        {
            get { return _notificationTemplateSelectedIndex;  }
            set { _notificationTemplateSelectedIndex = value; OnPropertyChanged("NotificationTemplateSelectedIndex"); }
        }

        private int _notificationTagSelectedIndex = 0;
        public int NotificationTagSelectedIndex
        {
            get { return _notificationTagSelectedIndex; }
            set { _notificationTagSelectedIndex = value; OnPropertyChanged("NotificationTagSelectedIndex"); }
        }

        private bool _sendNotificatonIsEnabled = true; 
        public bool SendNotificatonIsEnabled
        {
            get { return _sendNotificatonIsEnabled;  }
            set { _sendNotificatonIsEnabled = value; OnPropertyChanged("SendNotificationIsEnabled"); }
        }

        private ToastNotification _toastUpdate = new ToastNotification();
        public ToastNotification ToastUpdate
        {
            get { return _toastUpdate; }
            set { _toastUpdate = value; OnPropertyChanged("ToastNotification"); }
        }

        private TileNotification _tileUpdate = new TileNotification();
        public TileNotification TileUpdate
        {
            get { return _tileUpdate; }
            set { _tileUpdate = value; OnPropertyChanged("TileNotification"); }
        }

        private ObservableCollection<NotificationInfo> _notificationTag = new ObservableCollection<NotificationInfo>();
        public ObservableCollection<NotificationInfo> NotificationTag
        {
            get { return _notificationTag; }
            set { _notificationTag = value; OnPropertyChanged("NotificationTag"); }
        }

        private ObservableCollection<NotificationTemplate> _notificationTemplate = new ObservableCollection<NotificationTemplate>();
        public ObservableCollection<NotificationTemplate> NotificationTemplate
        {
            get { return _notificationTemplate; }
            set { _notificationTemplate = value; OnPropertyChanged("NotificationTemplate"); }
        }

        public MainWindowViewModel()
        {
            InitNotification();
            CreateReadTemplateData();
        }

        private async void CreateReadTemplateData()
        {
            if (Bl != null)
                NotificationTag = new ObservableCollection<NotificationInfo>(await Bl.LoadNotificationTagAsync());
            else
            {
                NotificationTag.Add(new NotificationInfo() { NotificationTag = "UserGroupAlert" });
                NotificationTag.Add(new NotificationInfo() { NotificationTag = "NextSession" });
            }

            ToastUpdate = new ToastNotification() { 
                ToastMessage = "Toast Headline", 
            };

            TileUpdate = new TileNotification() { 
                Headline = "Tile Headline",
                Line1 = "Tile Info 1",
                Line2 = "Tile Info 2",
                Line3 = "Tile Info 3"
            };

        }

        private void InitNotification()
        {
            NotificationTemplate.Add(new NotificationTemplate()
            {
                Name = "Toast",
                XmlTemplate = "<toast><visual><binding template=\"ToastText01\"><text id=\"1\">{0}</text></binding></visual></toast>",
            });
            NotificationTemplate.Add(new NotificationTemplate()
            {
                Name = "TileUpdate",
                XmlTemplate = "<tile><visual><binding template=\"TileSquareText01\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text></binding></visual></tile>",
            });
        }

        internal async void SendNotification()
        {
            NotificationInfo notificationTag = NotificationTag[NotificationTagSelectedIndex];
            NotificationTemplate notificationTemplate = NotificationTemplate[NotificationTemplateSelectedIndex];

            if (notificationTemplate.Name.ToUpper() == "TOAST")
            {
                await Bl.SendToastNotificationAsync(ToastUpdate.ToastMessage, notificationTemplate.XmlTemplate, notificationTag.NotificationTag); 
            }
            else
            {
                await Bl.SendTileNotificationAsync(TileUpdate, notificationTemplate.XmlTemplate, notificationTag.NotificationTag); 
            }
        }
    }
}
