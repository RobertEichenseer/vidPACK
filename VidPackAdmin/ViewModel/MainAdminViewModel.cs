using Apex.MVVM;
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
    public class MainAdminViewModel : BaseViewModel
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
            App.LocalConfiguration = _bl.ReadLocalConfiguration();

            //Command Notification
            _sendNotification = new Command(DoSendNotification); 
            
            //Command Setting
            _saveSetting = new Command(DoSaveSetting); 

        }

        /// <summary>
        /// View Model Data - Notifification
        /// </summary>

        private Command _sendNotification;
        public Command SendNotification
        {
            get { return _sendNotification; }
        }

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
        
        private bool _sendNotificationIsEnabled = true;
        public bool SendNotificationIsEnabled
        {
            get { return _sendNotificationIsEnabled; }
            set { _sendNotificationIsEnabled = value; OnPropertyChanged("SendNotificationIsEnabled"); }
        }

        private int _notificationTypeSelectedIndex;
        public int NotificationTypeSelectedIndex
        {
            get { return _notificationTypeSelectedIndex; }
            set 
            { 
                _notificationTypeSelectedIndex = value; 
                OnPropertyChanged("NotificationTypeSelectedIndex"); 

                ((NotificationViewModel)App.ViewModel["NotificationViewModel"]).MessageIsEnabled = NotificationType[_notificationTypeSelectedIndex].Name.ToUpper() != "TOAST";
                SendNotificationIsEnabled = CheckIfSendNotificationIsPossible(); 
            }
        }

        private int _notificationTagSelectedIndex = 0;
        public int NotificationTagSelectedIndex
        {
            get { return _notificationTagSelectedIndex; }
            set 
            { 
                _notificationTagSelectedIndex = value; 
                OnPropertyChanged("NotificationTagSelectedIndex");

                SendNotificationIsEnabled = CheckIfSendNotificationIsPossible(); 
            }
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

        private bool CheckIfSendNotificationIsPossible()
        {
            if (NotificationTypeSelectedIndex == -1)
                return false;
            if (NotificationTagSelectedIndex == -1)
                return false;

            NotificationViewModel viewModel = ((NotificationViewModel)App.ViewModel["NotificationViewModel"]);
            if (String.IsNullOrEmpty(viewModel.NotificationMessage.Headline.Trim()))
                return false;

            if (String.IsNullOrEmpty(viewModel.NotificationMessage.Message1.Trim()) && NotificationTypeSelectedIndex == 1)
                return false; 


            return true; 
        }



        /// <summary>
        /// View Model Data - Setting
        /// </summary>
        private bool _saveSettingIsEnabled = false;
        public bool SaveSettingIsEnabled 
        {
            get { return _saveSettingIsEnabled; }
            set { _saveSettingIsEnabled = value; OnPropertyChanged("SaveSettingIsEnabled"); } 
        }

        private Command _saveSetting;
        public Command SaveSetting
        {
            get { return _saveSetting; }
        }

        /// <summary>
        /// Action / Method
        /// </summary>
        private async void LoadExistingNotificationTag()
        {
            NotificationTag = new ObservableCollection<NotificationInfo>(await _bl.LoadNotificationTagAsync()); 
        }


        internal Control GetAdminAreaControl(int selectedAdminAreaIndex = -1)
        {
            if (selectedAdminAreaIndex != -1)
                SelectedAdminAreaIndex = selectedAdminAreaIndex; 

            switch (SelectedAdminAreaIndex)
            {
                case 0:
                {
                    return new Notification(_bl, this);
                }
                case 1:
                case 2:
                {
                    //not yet implemented
                    break;
                }

                case 3:
                {
                    return new Setting(_bl, this);
                }
            }
            return null; 
        }

        public void DoSaveSetting()
        {
            _bl.SaveLocalConfiguration(App.LocalConfiguration); 
        }

        public void DoSendNotification()
        {
            NotificationViewModel viewModel = App.ViewModel["NotificationViewModel"] as NotificationViewModel;
            viewModel.SendNotification(NotificationType[NotificationTypeSelectedIndex], NotificationTag[NotificationTagSelectedIndex].NotificationTag); 
        }


    }

}

