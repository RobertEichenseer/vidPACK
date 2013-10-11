using Callisto.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VidPackClient.Common;
using VidPackClient.View;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Networking.PushNotifications;
using Microsoft.WindowsAzure.Messaging;
using VidPackClient.ViewModel;
using VidPackClient.Bl;


// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace VidPackClient
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        //Define Notification Communication
        public static NotificationHub _NotificationHub = null;
        public static string _ChannelUri = ""; 

        //Define BL
        public static ICommonBl _Bl = new CommonBl_RestWebService();
        //public static ICommonBl _Bl = new CommonBl_MobileServices(); 

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand settingBackendUrl = new SettingsCommand("SettingBackendUrl", "Settings", (e) =>
            {
                SettingsFlyout settingsFlyout = new SettingsFlyout();
                settingsFlyout.FlyoutWidth = SettingsFlyout.SettingsFlyoutWidth.Narrow;
                settingsFlyout.HeaderText = "Settings";
                settingsFlyout.Content = new Settings();
                settingsFlyout.IsOpen = true; 
            });
            args.Request.ApplicationCommands.Add(settingBackendUrl);

            SettingsCommand settingPrivacyPolicy = new SettingsCommand("PrivacyPolicy", "PrivacyPolicy", (e) =>
            {
                SettingsFlyout settingsFlyout = new SettingsFlyout();
                settingsFlyout.FlyoutWidth = SettingsFlyout.SettingsFlyoutWidth.Narrow;
                settingsFlyout.HeaderText = "Privacy Policy";
                settingsFlyout.Content = new PrivacyPolicy();
                settingsFlyout.IsOpen = true;
            });
            args.Request.ApplicationCommands.Add(settingPrivacyPolicy); 

        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(Hub), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
                
            }
            // Ensure the current window is active
            Window.Current.Activate();

            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;

            //Register NotificationHub
            RegisterWithNotificationHub();
        }

        private async void RegisterWithNotificationHub()
        {
            _NotificationHub = new NotificationHub("vidpack", "Endpoint=sb://vidpack-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=p2N5fXEtJUJWohAgTWRUahBFYJWkSswH1GMogxpNP2Y=");
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            _ChannelUri = channel.Uri; 

            NotificationViewModel notificationViewModel = new NotificationViewModel(App._Bl);
            List<string> subscribedNotification = notificationViewModel.LoadSubscribedNotificationsLocal();

            await _NotificationHub.RegisterNativeAsync(_ChannelUri, subscribedNotification); 
            
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
