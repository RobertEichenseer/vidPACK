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

        public Setting(ICommonBl bl)
        {
            InitializeComponent();

            _bl = bl;
            _viewModel = new SettingViewModel(_bl);

            

            grdMain.DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var x = ""; 
        }
    }
}
