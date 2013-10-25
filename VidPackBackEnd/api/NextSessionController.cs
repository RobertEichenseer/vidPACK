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
    public class NextSessionController : ApiController
    {

        private string ThumbnailStorageUrl { get; set; }

        public NextSessionController()
        {
            ThumbnailStorageUrl = ""; 
        }

        // GET api/nextsession
        public Session Get()
        {
            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(retryStrategy);
            
            Session nextSession = new Session();
            using (VidPackEntities db = new VidPackEntities())
            {
                retryPolicy.ExecuteAction(() =>
                {
                    db.Database.Connection.Open();
                });

                ExistingSession dbSession = retryPolicy.ExecuteAction<ExistingSession>(() =>
                    db.ExistingSession.Where(item => item.IsNextSession == 1).FirstOrDefault()
                );

                if (dbSession != null)
                    nextSession = new VidPackModel.Session()
                    {
                        SessionDate = dbSession.SessionDate.ToString(),
                        SessionDescription = dbSession.SessionDescription,
                        SessionSubTitle = dbSession.SessionSubTitle,
                        SessionThumbnailUrl = String.Format("{0}{1}", ThumbnailStorageUrl, dbSession.SessionThumbnailUri),
                        SessionTitle = dbSession.SessionSubTitle,
                        SessionVideoUrl = dbSession.SessionVideoUri == null ? "" : dbSession.SessionVideoUri,
                        Speaker = dbSession.Speaker
                    };
            }
            return nextSession; 
        }

        // GET api/nextsession/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/nextsession
        public void Post([FromBody]string value)
        {
        }

        // PUT api/nextsession/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/nextsession/5
        public void Delete(int id)
        {
        }
    }
}
