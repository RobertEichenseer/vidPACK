using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl.MobileServiceDataType;
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
                         Id = item.Id, 
                         SessionDate = item.SessionDate.ToString(),
                         SessionDescription = item.SessionDescription,
                         SessionSubTitle = item.SessionSubTitle,
                         SessionThumbnailDisplayUrl = item.SessionThumbnailUri,
                         SessionThumbnailUrl = item.SessionThumbnailUri,
                         SessionTitle = item.SessionTitle,
                         SessionVideoUrl = item.SessionVideoUri,
                     }).ToList<Session>();

            #region - Mobile Service "Traditional" -
            foreach (Session session in actualSession)
            { 
                session.SessionDownloadItem = (await _mobileService.GetTable<MobileServiceDataType.DownloadItem>().Where(item => item.ExistingSession_Id == session.Id)
                                                .Select(item => new DownloadItemInfo() {
                                                    Caption = item.Caption,
                                                    Description = item.Description,
                                                    Url = item.Url, 
                                                })
                                                .ToListAsync()); 

            }
            #endregion - Mobile Service "Traditional"-

            #region - Mobile Service "VTable" -
            JToken jToken = (await _mobileService.GetTable("V_ExistingSession_DownloadItem").ReadAsync("Id=1"));
            var temp = jToken.ToObject<List<V_ExistingSession_DownloadItem>>();

            //var list = temp.GroupBy(p => p.SessionDescription, p => p,
            //    (key, g) => new { Title = key, OrderItem = g.ToList() }).ToList(); 

            //var temp2 = jToken.ToObject<List<DownloadItemInfo>>(); 
            
            //JToken jToken = await table.ReadAsync(String.Format("CustomerId={0}", customerId));
            //List<CustomOrderTypeRef> tempCustomOrder = jToken.ToObject<List<CustomOrderTypeRef>>();

            //var results = tempCustomOrder.GroupBy(p => p.CustomOrderId, p => p,
            //             (key, g) => new { CustomerId = key, OrderItem = g.ToList() }).ToList();


            //Customer specificCustomer = (await App.mobileServiceClient.GetTable<Customer>().Where(item => item.FirstName == "Robert").ToListAsync()).FirstOrDefault();
            ////Get specific customer by unique id
            //Customer specificCustomerLookup = (await App.mobileServiceClient.GetTable<Customer>().LookupAsync(1)); 
            ////Json
            //var specificCustomerJson = (await App.mobileServiceClient.GetTable("Customer").ReadAsync("FirstName == 'Robert'"));
            //var specificCustomerAsJson = (await App.mobileServiceClient.GetTable("Customer").LookupAsync(1)).Stringify();

            #endregion - Mobile Service "VTable" - 



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


