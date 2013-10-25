using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidPackBackEnd.EDMX;
using VidPackModel;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using Microsoft.Practices.TransientFaultHandling;


namespace VidPackBackEnd.api
{
    public class ActualSessionController : ApiController
    {
        private string ThumbnailStorageUrl { get; set; }

        public ActualSessionController()
        {
            ThumbnailStorageUrl = ""; 
        }

        // GET api/actualsession
        public Session Get()
        {

            var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            var retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(retryStrategy);

            Session actualSession = new Session(); 
            using (VidPackEntities db = new VidPackEntities())
            {
                retryPolicy.ExecuteAction(() =>
                {
                    db.Database.Connection.Open();
                });


                ExistingSession dbSession = retryPolicy.ExecuteAction<ExistingSession>(() =>
                    db.ExistingSession.Where(item => item.IsActualSession == 1).FirstOrDefault()
                ); 

                if (dbSession != null)
                    actualSession = new VidPackModel.Session()
                                        {
                                            SessionDate = Convert.ToString(dbSession.SessionDate),
                                            SessionDescription = dbSession.SessionDescription,
                                            SessionSubTitle = dbSession.SessionSubTitle,
                                            SessionThumbnailUrl = String.Format("{0}{1}",ThumbnailStorageUrl, dbSession.SessionThumbnailUri),
                                            SessionTitle = dbSession.SessionSubTitle,
                                            SessionVideoUrl = dbSession.SessionVideoUri,
                                            Speaker = dbSession.Speaker
                                        };
            }
            return actualSession; 
        }

        // GET api/actualsession/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/actualsession
        public void Post([FromBody]string value)
        {
        }

        // PUT api/actualsession/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/actualsession/5
        public void Delete(int id)
        {
        }
    }
}
