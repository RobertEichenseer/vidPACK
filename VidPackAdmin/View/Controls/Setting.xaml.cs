using Apex.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VidPackAdmin.Bl;
using VidPackAdmin.View.Controls;
using VidPackAdmin.ViewModel;

namespace VidPackAdmin.View.Controls
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : UserControl 
    {
        ICommonBl _bl;
        SettingViewModel _viewModel = null;
        MainAdminViewModel _mainAdminViewModel;

        
        internal Setting(ICommonBl bl, MainAdminViewModel mainAdminViewModel)
        {
            InitializeComponent();

            _bl = bl;
            _mainAdminViewModel = mainAdminViewModel;
            _viewModel = new SettingViewModel(_bl, _mainAdminViewModel);

            
            grdMain.DataContext = _viewModel;
        }

        private void SettingValue_KeyDown(object sender, KeyEventArgs e)
        {
            _mainAdminViewModel.SaveSettingIsEnabled = true; 
        }
    }
}
