using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidPackAdmin.ViewModel
{
    public class NotificationTemplate : BaseViewModel
    {
        private string _name = "";
        public string Name 
        {
            get { return _name;  }
            set { _name = value; OnPropertyChanged("Name"); } 
        }
        public string XmlTemplate { get; set; }
    }

    public class ToastNotification : BaseViewModel 
    {
        private string _toastMessage { get; set; }
        public string ToastMessage
        {
            get { return _toastMessage; }
            set { _toastMessage = value; OnPropertyChanged("ToastMessage"); }
        }

    }

    public class TileNotification : BaseViewModel
    {
        private string _headline = "";
        public string Headline 
        {
            get { return _headline; }
            set { _headline = value; OnPropertyChanged("Headline"); }
        }

        private string _line1 = "";
        public string Line1 
        {
            get { return _line1; }
            set { _line1 = value; OnPropertyChanged("Line1"); }
        }

        private string _line2 = "";
        public string Line2 
        {
            get { return _line2; }
            set { _line2 = value; OnPropertyChanged("Line2"); }
        }

        private string _line3 = "";
        public string Line3 
        {
            get { return _line3; }
            set { _line3 = value; OnPropertyChanged("Line3"); } 
        }
    }

}
