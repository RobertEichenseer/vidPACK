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
            if (String.IsNullOrEmpty(webServiceUrl))
                webServiceUrl = ""; 

            txtWebServiceUrl.Text = webServiceUrl; 
        }

        private void txtWebServiceUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;
            applicationDataContainer.Values["webServiceUrl"] = txtWebServiceUrl.Text; 
        }
    }
}
