using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using Microsoft.Practices.TransientFaultHandling;
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

            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(retryStrategy);

            List<NotificationInfo> notifications = new List<NotificationInfo>(); 
            using (VidPackEntities db = new VidPackEntities())
            {

                retryPolicy.ExecuteAction(() =>
                {
                    db.Database.Connection.Open();
                });

                notifications = retryPolicy.ExecuteAction<List<NotificationInfo>>(() =>
                        db.Notification.Select(item => new NotificationInfo() { 
                        NotificationTag = item.NotificationTag,
                    }).ToList<NotificationInfo>()
                ); 
            }
            return notifications; 
        }
    }
}
