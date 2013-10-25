using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl.MobileServiceDataType;
using VidPackClient.Common;
using VidPackModel;
using Windows.Data.Json; 

namespace VidPackClient.Bl
{
    class CommonBl_MobileServices : ICommonBl 
    {

        public static MobileServiceClient _mobileService; 

        public void SetConfigPara(VidPackModel.ClientConfig clientConfig)
        {
            _mobileService = new MobileServiceClient(clientConfig.MobileServiceUrl, clientConfig.MobileServiceApplicationKey); 
        }

        public async Task<Session> LoadActualSession()
        {
            Session actualSession = new Session(); 
            try
            {
                actualSession = (await _mobileService.GetTable<ExistingSession>().ReadAsync())
                                    .Where(item => item.IsActualSession == 1)
                                    .Select(item => new Session
                                    {
                                        SessionDate = item.SessionDate.ToString(),
                                        SessionDescription = item.SessionDescription,
                                        SessionSubTitle = item.SessionSubTitle,
                                        SessionThumbnailDisplayUrl = item.SessionThumbnailUri,
                                        SessionThumbnailUrl = item.SessionThumbnailUri,
                                        SessionTitle = item.SessionTitle,
                                        SessionVideoUrl = item.SessionVideoUri,
                                    }).FirstOrDefault();
            }
            catch (Exception exception)
            {
                ErrorHandler.AddError(exception);
            }

            return actualSession; 

        }

        public async Task<VidPackModel.Session> LoadNextSession()
        {
            Session nextSession = new Session();

            try
            {
                nextSession = (await _mobileService.GetTable<ExistingSession>().ReadAsync())
                         .Where(item => item.IsActualSession == 1)
                         .Select(item => new Session
                         {
                             SessionDate = item.SessionDate.ToString(),
                             SessionDescription = item.SessionDescription,
                             SessionSubTitle = item.SessionSubTitle,
                             SessionThumbnailDisplayUrl = item.SessionThumbnailUri,
                             SessionThumbnailUrl = item.SessionThumbnailUri,
                             SessionTitle = item.SessionTitle,
                             SessionVideoUrl = item.SessionVideoUri,
                         }).FirstOrDefault();
            }
            catch (Exception exception)
            {
                ErrorHandler.AddError(exception);
            }
            return nextSession; 
        }

        public async Task<List<VidPackModel.Session>> LoadPastSession()
        {
            List<Session> actualSession; 

            #region - Mobile Service "Traditional" -
            //actualSession = (await _mobileService.GetTable<ExistingSession>().ReadAsync())
            //         .Select(item => new Session
            //         {
            //             Id = item.Id, 
            //             SessionDate = item.SessionDate.ToString(),
            //             SessionDescription = item.SessionDescription,
            //             SessionSubTitle = item.SessionSubTitle,
            //             SessionThumbnailDisplayUrl = item.SessionThumbnailUri,
            //             SessionThumbnailUrl = item.SessionThumbnailUri,
            //             SessionTitle = item.SessionTitle,
            //             SessionVideoUrl = item.SessionVideoUri,
            //         }).ToList<Session>();

            //#region - Mobile Service "Traditional" Child Records -
            //foreach (Session session in actualSession)
            //{ 
            //    session.SessionDownloadItem = (await _mobileService.GetTable<MobileServiceDataType.DownloadItem>().Where(item => item.ExistingSession_Id == session.Id)
            //                                    .Select(item => new DownloadItemInfo() {
            //                                        Caption = item.Caption,
            //                                        Description = item.Description,
            //                                        Url = item.Url, 
            //                                    })
            //                                    .ToListAsync()); 

            //}
            //#endregion - Mobile Service "Traditional" Child Records -
            #endregion - Mobile Service "Traditional"-

            #region - Mobile Service "VTable" -
            //var sessionRawInfo = (await _mobileService.GetTable<V_ExistingSession_DownloadItem>().ToListAsync());
            //var sessionGrouped = sessionRawInfo.GroupBy(p => p.SessionDescription, p => p,
            //    (key, g) => new
            //    {
            //        SessionTitle = g.ToList()[0].SessionTitle,
            //        SessionSubTitle = g.ToList()[0].SessionSubTitle,
            //        SessionDate = g.ToList()[0].SessionDate,
            //        Speaker = g.ToList()[0].Speaker,
            //        SessionDescription = g.ToList()[0].SessionDescription,
            //        SessionVideoUri = g.ToList()[0].SessionVideoUri,
            //        SessionThumbnailUrl = g.ToList()[0].SessionThumbnailUri,
            //        SessionThumbnailDisplayUrl = "ms-appx:///Assets/SessionPlaceholder.jpg",
            //        IsActualSession = g.ToList()[0].IsActualSession,
            //        IsNextSession = g.ToList()[0].IsNextSession,
            //        DownloadItems = g.ToList().Select(item => new
            //        {
            //            Caption = item.Caption,
            //            Description = item.Description,
            //            Url = item.Url
            //        }).ToList(),
            //    }).ToList();

            //actualSession = sessionGrouped.Select(item => new Session()
            //{
            //    SessionDate = item.SessionDate.ToString(),
            //    SessionDescription = item.SessionDescription,
            //    SessionSubTitle = item.SessionSubTitle,
            //    SessionThumbnailDisplayUrl = item.SessionThumbnailDisplayUrl,
            //    SessionThumbnailUrl = item.SessionThumbnailUrl,
            //    SessionTitle = item.SessionTitle,
            //    SessionVideoUrl = item.SessionVideoUri,
            //    Speaker = item.Speaker,
            //    SessionDownloadItem = item.DownloadItems.Select(_ => new DownloadItemInfo()
            //    {
            //        Caption = _.Caption,
            //        Description = _.Description,
            //        Url = _.Url,
            //    }).ToList<DownloadItemInfo>(),
            //}).ToList<Session>(); 

            //Get json Info
            //JToken jToken = (await _mobileService.GetTable("V_ExistingSession_DownloadItem").ReadAsync(""));
            //var sessions = jToken.ToObject<List<V_ExistingSession_DownloadItem>>();
            #endregion - Mobile Service "VTable" - 

            #region - Mobile Service API -
            var arguments = new Dictionary<string, string>();
            List<V_ExistingSession_DownloadItem> sessionRawInfoApi = new List<V_ExistingSession_DownloadItem>(); 
            try
            {
                sessionRawInfoApi = (await _mobileService.InvokeApiAsync<List<V_ExistingSession_DownloadItem>>("v_existingsession_downloaditem", HttpMethod.Get, arguments));
            }
            catch (Exception exception)
            {
                ErrorHandler.AddError(exception);
                return new List<Session>(); 
            }

            var sessionGroupedApi = sessionRawInfoApi.GroupBy(p => p.SessionDescription, p => p,
                (key, g) => new
                {
                    SessionTitle = g.ToList()[0].SessionTitle,
                    SessionSubTitle = g.ToList()[0].SessionSubTitle,
                    SessionDate = g.ToList()[0].SessionDate,
                    Speaker = g.ToList()[0].Speaker,
                    SessionDescription = g.ToList()[0].SessionDescription,
                    SessionVideoUri = g.ToList()[0].SessionVideoUri,
                    SessionThumbnailUrl = g.ToList()[0].SessionThumbnailUri,
                    SessionThumbnailDisplayUrl = "ms-appx:///Assets/SessionPlaceholder.jpg",
                    IsActualSession = g.ToList()[0].IsActualSession,
                    IsNextSession = g.ToList()[0].IsNextSession,
                    DownloadItems = g.ToList().Select(item => new
                    {
                        Caption = item.Caption,
                        Description = item.Description,
                        Url = item.Url
                    }).ToList(),
                }).ToList();

            actualSession = sessionGroupedApi.Select(item => new Session()
            {
                SessionDate = item.SessionDate.ToString(),
                SessionDescription = item.SessionDescription,
                SessionSubTitle = item.SessionSubTitle,
                SessionThumbnailDisplayUrl = item.SessionThumbnailDisplayUrl,
                SessionThumbnailUrl = item.SessionThumbnailUrl,
                SessionTitle = item.SessionTitle,
                SessionVideoUrl = item.SessionVideoUri,
                Speaker = item.Speaker,
                SessionDownloadItem = item.DownloadItems.Select(_ => new DownloadItemInfo()
                {
                    Caption = _.Caption,
                    Description = _.Description,
                    Url = _.Url,
                }).ToList<DownloadItemInfo>(),
            }).ToList<Session>(); 
            #endregion - Mobile Service API -

            return actualSession; 
        }

        public async Task<List<VidPackModel.NotificationInfo>> LoadNotifications()
        {
            var notifications = (await _mobileService.GetTable<Notification>().ReadAsync())
                     .Select(item => new NotificationInfo
                     {
                         IsSubscribed = false,
                         NotificationTag = item.NotificationTag
                     }).ToList<NotificationInfo>();

            return notifications; 
        }
    }
}


