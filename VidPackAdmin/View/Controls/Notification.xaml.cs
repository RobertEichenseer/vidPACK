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
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl   
    {
        ICommonBl _bl;
        NotificationViewModel _viewModel = null;
        MainAdminViewModel _mainAdminViewModel;

        public Notification(ICommonBl bl, MainAdminViewModel mainAdminViewModel)
        {
            InitializeComponent();

            _bl = bl;
            _mainAdminViewModel = mainAdminViewModel;

            _viewModel = new NotificationViewModel(_bl, _mainAdminViewModel);
            App.AddViewModel("NotificationViewModel", _viewModel); 

            grdMain.DataContext = _viewModel;

        }

        private void Message_KeyUp(object sender, KeyEventArgs e)
        {
            MainAdminViewModel mainAdminViewModel = ((MainAdminViewModel)App.ViewModel["MainAdminViewModel"]);
            mainAdminViewModel.SendNotificationIsEnabled = mainAdminViewModel.CheckIfSendNotificationIsPossible(); 
        }

    }
}
