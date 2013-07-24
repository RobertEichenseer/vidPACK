using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidPackBackEnd.EDMX;
using VidPackModel;

namespace VidPackBackEnd.api
{
    public class NotificationController : ApiController
    {
        // GET api/notification
        public IEnumerable<NotificationInfo> Get()
        {
            List<NotificationInfo> notifications = new List<NotificationInfo>(); 
            using (VidPackEntities db = new VidPackEntities())
            {
                notifications = db.Notification.Select(item => new NotificationInfo() { 
                    NotificationTag = item.NotificationTag,
                }).ToList<NotificationInfo>(); 
            }
            return notifications; 
        }
    }
}
