using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;
using VidPackBackEnd.EDMX;
using VidPackModel;

namespace VidPackBackEnd.api
{
    public class SessionController : ApiController
    {
        private string ThumbnailStorageUrl { get; set; }

        public SessionController()
        {
            ThumbnailStorageUrl = ""; 
        }

        // GET api/session
        public IEnumerable<Session> Get(ODataQueryOptions<Session> queryOptions)
        {
            List<Session> session = new List<Session>();
            try
            {
                using (VidPackEntities db = new VidPackEntities())
                {
                    var listSession = db.ExistingSession.Include("DownloadItem").Select(item => new
                    {
                        SessionDate = item.SessionDate,
                        SessionDescription = item.SessionDescription,
                        SessionSubTitle = item.SessionSubTitle,
                        SessionThumbnailUrl = item.SessionThumbnailUri,
                        SessionTitle = item.SessionSubTitle,
                        SessionVideoUrl = item.SessionVideoUri == null ? "" : item.SessionVideoUri,
                        Speaker = item.Speaker,
                        isActual = item.IsActualSession,
                        DownloadItem = item.DownloadItem.Select(_ => new
                        {
                            Caption = _.Caption,
                            Description = _.Description,
                            Url = _.Url,
                        })
                    }).ToList();

                    //ToString() / String.Format() is not know within projection
                    session = listSession.Select(item => new Session() {
                        SessionDate = Convert.ToString(item.SessionDate),
                        SessionDescription = item.SessionDescription,
                        SessionSubTitle = item.SessionSubTitle,
                        SessionThumbnailUrl = String.Format("{0}{1}", ThumbnailStorageUrl, item.SessionThumbnailUrl),
                        SessionTitle = item.SessionSubTitle,
                        SessionVideoUrl = item.SessionVideoUrl,
                        Speaker = item.Speaker,
                        SessionDownloadItem = item.DownloadItem.Select(_ => new DownloadItemInfo() {
                            Caption = _.Caption,
                            Description = _.Description,
                            Url = _.Url,
                        }).ToList<DownloadItemInfo>(),
                    }).ToList<Session>();

                    return session;
                }
            }
            catch (Exception ex)
            {
                //TODO: Implement Error Handling
                return new List<Session>();
            }
        }

        // GET api/session/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/session
        public void Post([FromBody]string value)
        {
        }

        // PUT api/session/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/session/5
        public void Delete(int id)
        {
        }
    }
}
