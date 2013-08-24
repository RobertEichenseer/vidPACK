using Microsoft.ServiceBus.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VidPackAdmin.ViewModel;

namespace VidPackAdmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LocalConfigurationInfo LocalConfiguration { get; set; }
        public static Dictionary<string, BaseViewModel> ViewModel { get; set; }

        public App()
        {
            ViewModel = new Dictionary<string, BaseViewModel>(); 
        }

        public static void AddViewModel(string viewModelName, BaseViewModel viewModel)
        {
            if (ViewModel.ContainsKey(viewModelName))
            {
                ViewModel[viewModelName] = viewModel;
            }
            else 
            {
                ViewModel.Add(viewModelName, viewModel); 
            }
        }
    }
}
