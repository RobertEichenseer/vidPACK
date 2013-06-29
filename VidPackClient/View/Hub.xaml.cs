using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VidPackClient.Bl;
using VidPackClient.BL;
using VidPackClient.View;
using VidPackClient.ViewModel;
using VidPackModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
 

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace VidPackClient
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Hub : VidPackClient.Common.LayoutAwarePage
    {

        HubViewModel _viewModel = new HubViewModel(new CommonBl_RestWebService()); 

        public Hub()
        {
            
            this.InitializeComponent();
            grdMain.DataContext = _viewModel;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //ListBox Sizing
            //lbDownloads.Height= grdContent.ActualHeight - 160;

            _viewModel.LoadHubContent();
            
        }

        private void PastSession_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            Session selectedSession = (Session)e.AddedItems[0];
            Frame.Navigate(typeof(SessionDetail), new SessionDetailInputPara() { SelectedSession = selectedSession, Bl = _viewModel._bl }); 
        }
    }
}
