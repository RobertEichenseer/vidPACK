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
using VidPackAdmin.ViewModel;

namespace VidPackAdmin.View.Controls
{
    /// <summary>
    /// Interaction logic for Video.xaml
    /// </summary>
    public partial class Video : UserControl
    {
        ICommonBl _bl;
        VideoViewModel _viewModel = null;
        MainAdminViewModel _mainAdminViewModel;

        
        internal Video(ICommonBl bl, MainAdminViewModel mainAdminViewModel)
        {
            InitializeComponent();

            _bl = bl;
            _mainAdminViewModel = mainAdminViewModel;
            _viewModel = new VideoViewModel(_bl, _mainAdminViewModel);

            
            grdMain.DataContext = _viewModel;
        }
    }
}
