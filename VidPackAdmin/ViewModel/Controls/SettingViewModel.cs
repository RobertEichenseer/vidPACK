using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackAdmin.Bl;
using System.Configuration; 

namespace VidPackAdmin.ViewModel
{
    class SettingViewModel : BaseViewModel
    {
        ICommonBl _bl; 

        public SettingViewModel(ICommonBl bl)
        {
            _bl = bl;
            ReadLocalConfiguration(); 
        }

        private LocalConfigurationInfo _localConfiguration;
        public LocalConfigurationInfo LocalConfiguration
        {
            get { return _localConfiguration; }
            set { _localConfiguration = value; OnPropertyChanged("LocalConfiguration"); }
        }


        private void ReadLocalConfiguration()
        {
            LocalConfiguration = _bl.ReadLocalConfiguration();

            if (String.IsNullOrEmpty(LocalConfiguration.BackendUrl))
                LocalConfiguration.BackendUrl = "http://vidpack.azurewebsites.net"; 
        }
    }
}
