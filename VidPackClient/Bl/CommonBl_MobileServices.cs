using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl.MobileServiceDataType;
using VidPackModel;

namespace VidPackClient.Bl
{
    class CommonBl_MobileServices : ICommonBl 
    {

        //public static MobileServiceClient _mobileService = new MobileServiceClient("https://vidpackstaging.azure-mobile.net/","FnIlICvSGhjXlggDLhtCPiGpNYDoti15");
        public static MobileServiceClient _mobileService; 

        public void SetConfigPara(VidPackModel.ClientConfig clientConfig)
        {
            _mobileService = new MobileServiceClient(clientConfig.MobileServiceUrl, clientConfig.MobileServiceApplicationKey); 
        }

        public async Task<Session> LoadActualSession()
        {
            var actualSession = (await _mobileService.GetTable<ExistingSession>().ReadAsync())
                                .Where(item => item.IsActualSession == 1)
                                .Select(item => new Session {
                                    SessionDate = item.SessionDate.ToString(),
                                    SessionDescription = item.SessionDescription,
                                    SessionSubTitle = item.SessionSubTitle, 
                                    SessionThumbnailDisplayUrl = item.SessionThumbnailUri,
                                    SessionThumbnailUrl = item.SessionThumbnailUri, 
                                    SessionTitle = item.SessionTitle,
                                    SessionVideoUrl = item.SessionVideoUri,
                                }).FirstOrDefault();

            return actualSession; 

        }

        public async Task<VidPackModel.Session> LoadNextSession()
        {
            var nextSession = (await _mobileService.GetTable<ExistingSession>().ReadAsync())
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

            return nextSession; 
        }

        public async Task<List<VidPackModel.Session>> LoadPastSession()
        {
            var actualSession = (await _mobileService.GetTable<ExistingSession>().ReadAsync())
                     .Select(item => new Session
                     {
                         SessionDate = item.SessionDate.ToString(),
                         SessionDescription = item.SessionDescription,
                         SessionSubTitle = item.SessionSubTitle,
                         SessionThumbnailDisplayUrl = item.SessionThumbnailUri,
                         SessionThumbnailUrl = item.SessionThumbnailUri,
                         SessionTitle = item.SessionTitle,
                         SessionVideoUrl = item.SessionVideoUri,
                     }).ToList<Session>();

            Session returnValue = new Session();
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


