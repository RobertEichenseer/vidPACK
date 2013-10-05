using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidPackClient.Bl.MobileServiceDataType
{

    public class ExistingSession
    {
        public ExistingSession()
        {
            this.SessionThumbnailDisplayUrl = "ms-appx:///Assets/SessionPlaceholder.jpg";
        }

        public int Id { get; set; }
        public string SessionTitle { get; set; }
        public string SessionSubTitle { get; set; }
        public DateTime SessionDate { get; set; }
        public string Speaker { get; set; }
        public string SessionDescription { get; set; }
        public string SessionVideoUri { get; set; }
        public string SessionThumbnailUri { get; set; }
        public string SessionThumbnailDisplayUrl { get; set; }
        public int IsActualSession { get; set; }
        public int IsNextSession { get; set; }
    }

    public class Notification
    {
        public int Id { get; set; }
        public string NotificationTag { get; set; }
    }

    public class DownloadItem
    {
        public int Id { get; set; }
        public int ExistingSession_Id { get; set; }
        public string Caption {get; set;}
        public string Description {get; set;}
        public string Url { get; set; }
    }

    public class V_ExistingSession_DownloadItem
    {
        int ExistingSessionId { get; set; }
        string SessionTitle { get; set; }
        string SessionSubTitle { get; set;}
        DateTime SessionDate {get; set;}
        string Speaker { get; set; }
        string SessionDescription { get; set; } 
        string SessionVideoUri { get; set; }
        string SessionThumbnailUri { get; set; } 
        string IsActualSession { get; set; }
        int IsNextSession { get; set; }

        int DownloadItemId { get; set; }
        string Caption { get; set; } 
        string Description { get; set; }
        string Url { get; set; } 
    }

}
