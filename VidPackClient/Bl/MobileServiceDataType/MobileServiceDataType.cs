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

}
