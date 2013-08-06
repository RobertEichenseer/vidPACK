using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl;
using VidPackClient.Common;
using VidPackModel;

namespace VidPackClient.ViewModel
{
    class SessionDetailViewModel : BindableBase
    {
        public Session _selectedSession;
        public Session SelectedSession 
        {
            get {return _selectedSession; }
            set {_selectedSession = value; OnPropertyChanged("SelectedSession"); }
        }


        public ICommonBl Bl { get; set; }
        public SessionDetailViewModel(ICommonBl bl)
        {
            Bl = bl; 
        }

        //Constructor without BL used during Designtime
        public SessionDetailViewModel ()
	    {
            SelectedSession = new Session(){SessionTitle = "TFS - Alles Einstellungssache", SessionSubTitle = "Anpassungen am Team Foundation Server für agile Teams", SessionDate = "2013-06-28", Speaker = "Karsten Kempe", SessionDescription = "Agilität gewinnt in der Software-Entwicklung immer mehr an Bedeutung. Unterstützung finden viele Unternehmen durch den Team Foundation Server. Dieser liefert einige Prozess-Templates mit, die eine stabile Basis für jedes agile Entwicklungsteam bilden. Aber manchmal reichen die Möglichkeiten dieser Standard-Templates nicht aus, um auch speziellere Anforderungen der Entwicklerteams zu erfüllen. In diesem Vortrag werfen Sie einen Blick auf die inneren Werte eines Prozess Templates. Sie erfahren Dos and Don'ts bei deren Erweiterung, passen ein Prozess-Template nach Ihren Wünschen an und konfigurieren den Web Access des Team Foundation Servers", SessionVideoUrl= "https://mstechdemo.blob.core.windows.net/asset-9544b543-72eb-41f4-acb9-2d0ea700e526/Karsten_UserGroup_ALM.mp4?sv=2012-02-12&st=2013-06-04T06%3A15%3A57Z&se=2015-06-04T06%3A15%3A57Z&sr=c&si=f81a4023-c0a7-4117-a37b-36f0afe3a78f&sig=IAhoSeyQCbyf0Ff0jXPCHs98dWT69Hvl4AvI6StS7Zw%3D"}; 
	    }

    }
}
