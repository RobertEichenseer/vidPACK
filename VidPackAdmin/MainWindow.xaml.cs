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

namespace VidPackAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel _ViewModel = null;

        public MainWindow()
        {
            InitializeComponent();
            _ViewModel = grdMain.DataContext as MainWindowViewModel; 
            _ViewModel.Bl = new CommonBl_RestWebService(); 
        }

        private void SendNotificaton_Click(object sender, RoutedEventArgs e)
        {
            _ViewModel.SendNotification(); 
        }
    }
}
