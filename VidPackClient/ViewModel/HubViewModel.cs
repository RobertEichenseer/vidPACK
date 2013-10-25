using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl;
using VidPackClient.Common;
using VidPackModel;
using Windows.Storage;
using Windows.UI.Popups;

namespace VidPackClient.ViewModel
{
    class HubViewModel : BindableBase
    {
        private ObservableCollection<Session> _sessions;
        public ObservableCollection<Session> Sessions
        { 
            get {return _sessions;}
            set { _sessions = value; OnPropertyChanged("Sessions"); } 
        }


        public ObservableCollection<DownloadItemInfo> DownloadItems { get; set; }

        private Session _nextSession { get; set; }
        public Session NextSession 
        {
            get { return _nextSession;  }
            set { _nextSession = value; OnPropertyChanged("NextSession"); } 
        }

        private Session _actualSession { get; set; }
        public Session ActualSession
        {
            get { return _actualSession; }
            set { _actualSession = value; OnPropertyChanged("ActualSession"); }
        }

        //Constructor with BusinessLogic
        public ICommonBl _bl {get; set;} 
        public HubViewModel(ICommonBl bl)
        {
            _bl = bl;

            ReadConfigParameter();
            
            Sessions = new ObservableCollection<Session>();
            DownloadItems = new ObservableCollection<DownloadItemInfo>(); 
        }

        //Called from Design Mode - Populate Some Demo Data
        public HubViewModel()
        {
            Sessions = new ObservableCollection<Session>();
            DownloadItems = new ObservableCollection<DownloadItemInfo>(); 
            
            Sessions.Add(new Session() { SessionTitle="From Zero To Azure Hero", SessionSubTitle="SubTitle", SessionDate="2013-06-28", Speaker="Robert Eichenseer" });
            Sessions.Add(new Session() { SessionTitle = "Win8 jetzt auch mit speichern", SessionSubTitle = "SubTitle", SessionDate = "2013-06-28", Speaker = "Robert Eichenseer" });
            Sessions.Add(new Session() { SessionTitle = "Windows Azure ohne Windows", SessionSubTitle = "SubTitle", SessionDate = "2013-06-28", Speaker = "Robert Eichenseer" });

            NextSession = new Session() { SessionTitle = "The big next Session", SessionSubTitle = "Subtitle of the next session", SessionDate ="2013-10-28", Speaker = "Robert Eichenseer", SessionDescription = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet." };
            ActualSession = new Session() { SessionTitle = "TFS - Alles Einstellungssache", SessionSubTitle = "Anpassungen am Team Foundation Server für agile Teams", SessionDate = "2013-06-28", Speaker = "Karsten Kempe", SessionDescription = "Agilität gewinnt in der Software-Entwicklung immer mehr an Bedeutung. Unterstützung finden viele Unternehmen durch den Team Foundation Server. Dieser liefert einige Prozess-Templates mit, die eine stabile Basis für jedes agile Entwicklungsteam bilden. Aber manchmal reichen die Möglichkeiten dieser Standard-Templates nicht aus, um auch speziellere Anforderungen der Entwicklerteams zu erfüllen. In diesem Vortrag werfen Sie einen Blick auf die inneren Werte eines Prozess Templates. Sie erfahren Dos and Don'ts bei deren Erweiterung, passen ein Prozess-Template nach Ihren Wünschen an und konfigurieren den Web Access des Team Foundation Servers", SessionVideoUrl= "https://mstechdemo.blob.core.windows.net/asset-9544b543-72eb-41f4-acb9-2d0ea700e526/Karsten_UserGroup_ALM.mp4?sv=2012-02-12&st=2013-06-04T06%3A15%3A57Z&se=2015-06-04T06%3A15%3A57Z&sr=c&si=f81a4023-c0a7-4117-a37b-36f0afe3a78f&sig=IAhoSeyQCbyf0Ff0jXPCHs98dWT69Hvl4AvI6StS7Zw%3D" };

            for (int i = 0; i < 20; i++)
            {
                DownloadItems.Add(new DownloadItemInfo() { Caption = String.Format("Download Item {0}",i.ToString()), Url = "http://www.download.com", Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet." });
            }
        }

        //Event Handling
        internal async void LoadHubContent()
        {
            ErrorHandler.RethrowError = true;
            try
            {
                ActualSession = await _bl.LoadActualSession();
                NextSession = await _bl.LoadNextSession();
                Sessions = new ObservableCollection<Session>(await _bl.LoadPastSession());

                //Async load of Speaker Images;
                Sessions = await Task.Run<ObservableCollection<Session>>(() =>
                {
                    //Todo: Integration of error handling
                    foreach (Session session in Sessions)
                        session.SessionThumbnailDisplayUrl = session.SessionThumbnailUrl;

                    return Sessions;
                });
            }
            catch (Exception exception)
            {
                ErrorHandler.ShowLastError("Bitte prüfen Sie die Verbindung zum Backend!"); 
            }

        }

        private void ReadConfigParameter()
        {
            ApplicationDataContainer applicationDataContainer = ApplicationData.Current.LocalSettings;

           
            string webServiceUrl = applicationDataContainer.Values["webServiceUrl"] as string;
            if (String.IsNullOrEmpty(webServiceUrl))
            {
                webServiceUrl = "http://vidpack.azurewebsites.net/api/";
                applicationDataContainer.Values["webServiceUrl"] = webServiceUrl;
            }

            string mobileServiceUrl = applicationDataContainer.Values["mobileServiceUrl"] as string;
            if (String.IsNullOrEmpty(mobileServiceUrl))
            {
                mobileServiceUrl = "https://vidpackstaging.azure-mobile.net/";
                applicationDataContainer.Values["mobileServiceUrl"] = mobileServiceUrl; 
            }

            string mobileApplicationKey = applicationDataContainer.Values["mobileApplicationKey"] as string;
            if (String.IsNullOrEmpty(mobileApplicationKey))
            {
                mobileApplicationKey = "FnIlICvSGhjXlggDLhtCPiGpNYDoti15";
                applicationDataContainer.Values["mobileApplicationKey"] = mobileApplicationKey; 
            }

            //Debug Settings
            webServiceUrl = "http://vidpackstaging.azurewebsites.net/api/";
            //mobileServiceUrl = "https://vidpackstaging.azure-mobile.net/";
            //mobileApplicationKey = "FnIlICvSGhjXlggDLhtCPiGpNYDoti15"; 

            _bl.SetConfigPara(new ClientConfig() { 
                WebServiceUrl = webServiceUrl, 
                MobileServiceUrl = mobileServiceUrl,
                MobileServiceApplicationKey = mobileApplicationKey,
            }); 
        }
    }

}
