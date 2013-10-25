using System;
using System.Collections.Generic;
using System.Text;

namespace VidPackModel
{
    public class Session
    {

        public Session()
        {
            this.SessionThumbnailDisplayUrl = "ms-appx:///Assets/SessionPlaceholder.jpg"; 
        }

        public int Id { get; set; }
        public string SessionTitle { get; set; }
        public string SessionSubTitle { get; set; }
        public string SessionDate { get; set; }
        public string Speaker { get; set; }
        public string SessionDescription { get; set; }
        public string SessionVideoUrl { get; set; }
        public string SessionThumbnailUrl { get; set; }
        public string SessionThumbnailDisplayUrl { get; set; }
        public List<DownloadItemInfo> SessionDownloadItem { get; set; }
    }

    public class DownloadItemInfo
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public class NotificationInfo
    {
        public string NotificationTag { get; set; }
        public bool IsSubscribed { get; set; }
    }

    public class ClientConfig
    {
        public string WebServiceUrl { get; set; }

        public string MobileServiceUrl { get; set; }
        public string MobileServiceApplicationKey { get; set; }
    }
}
