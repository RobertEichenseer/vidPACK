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
    public class ActualSessionController : ApiController
    {


        private string ThumbnailStorageUrl { get; set; }

        public ActualSessionController()
        {
            ThumbnailStorageUrl = "https://somedomain/"; 
        }

        // GET api/actualsession
        public Session Get()
        {
            Session actualSession = new Session(); 
            using (VidPackEntities db = new VidPackEntities())
            {
                ExistingSession dbSession = db.ExistingSession.Where(item => item.IsActualSession == 1).FirstOrDefault();

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
