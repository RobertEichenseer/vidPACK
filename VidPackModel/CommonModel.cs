using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VidPackModel
{
    public class Session
    {

        public Session()
        {
            this.SessionThumbnailDisplayUrl = "ms-appx:///Assets/SessionPlaceholder.jpg"; 
        }

        public string SessionTitle { get; set; }
        public string SessionSubTitle { get; set; }
        public string SessionDate { get; set; }
        public string Speaker { get; set; }
        public string SessionDescription { get; set; }
        public string SessionVideoUrl { get; set; }
        public string SessionThumbnailUrl { get; set; }
        public string SessionThumbnailDisplayUrl { get; set; }
    }

    public class DownloadItem
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
