using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VidPackClient.View
{
    public sealed partial class Settings : UserControl
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            string webServiceUrl = applicationDataContainer.Values["webServiceUrl"] as string;
            string mobileServiceUrl = applicationDataContainer.Values["mobileServiceUrl"] as string;
            string mobileServiceAppKey = applicationDataContainer.Values["mobileServiceAppKey"] as string;

            if (String.IsNullOrEmpty(webServiceUrl))
                webServiceUrl = "";

            if (String.IsNullOrEmpty(mobileServiceUrl))
                mobileServiceUrl = "";

            if (String.IsNullOrEmpty(mobileServiceAppKey))
                mobileServiceAppKey = "";

            txtWebServiceUrl.Text = webServiceUrl;
            txtMobileServiceUrl.Text = mobileServiceUrl;
            txtMobileServiceAppKey.Text = mobileServiceAppKey;
        }

        private void txtWebServiceUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            applicationDataContainer.Values["webServiceUrl"] = txtWebServiceUrl.Text; 
        }

        private void txtMobileServiceUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            applicationDataContainer.Values["mobileServiceUrl"] = txtMobileServiceUrl.Text; 
        }

        private void txtMobileServiceAppKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            applicationDataContainer.Values["mobileServiceAppKey"] = txtMobileServiceAppKey.Text; 
        }

        

        

    }
}
