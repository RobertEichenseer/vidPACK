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
using System.Windows.Shapes;
using VidPackAdmin.Bl;
using VidPackAdmin.View.Controls;
using VidPackAdmin.ViewModel;

namespace VidPackAdmin.View
{
    /// <summary>
    /// Interaction logic for MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {

        private MainAdminViewModel _viewModel; 

        public MainAdmin()
        {
            InitializeComponent();
            _viewModel = new MainAdminViewModel(new CommonBl_RestWebService());
            grdMain.DataContext = _viewModel;
            ribMain.SelectionChanged += ribMain_SelectionChanged;
        }

        void ribMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Control adminAreaControl = _viewModel.GetAdminAreaControl();
            ContentHost.Child = adminAreaControl; 
        }
    }
}
