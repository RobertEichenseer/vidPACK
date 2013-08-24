using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackAdmin.Bl;
using System.Configuration;
using Apex.MVVM; 

namespace VidPackAdmin.ViewModel
{
    class SettingViewModel : BaseViewModel
    {
        ICommonBl _bl;
        MainAdminViewModel _mainAdminViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bl"> Business Logic</param>
        /// <param name="mainAdminViewModel"></param>
        public SettingViewModel(ICommonBl bl, MainAdminViewModel mainAdminViewModel)
        {
            _bl = bl;
            _mainAdminViewModel = mainAdminViewModel;

        }

        /// <summary>
        /// View Data
        /// </summary>
        public LocalConfigurationInfo LocalConfiguration
        {
            get { return App.LocalConfiguration; }
            set { App.LocalConfiguration = value; OnPropertyChanged("LocalConfiguration"); }
        }

    }
}
