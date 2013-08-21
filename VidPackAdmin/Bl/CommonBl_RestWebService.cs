using Microsoft.ServiceBus.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VidPackAdmin.ViewModel;
using VidPackModel;

namespace VidPackAdmin.Bl
{
    class CommonBl_RestWebService : ICommonBl
    {
        public static NotificationHubClient _NotificationHubClient;
        //public Uri _webServiceUri = new Uri("http://localhost:19513/api/");
        public Uri _webServiceUri = new Uri("http://vidpack.azurewebsites.net/api/");

        public CommonBl_RestWebService()
        {
            string connectionString = "Endpoint=sb://vidpack-ns.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=+pKvadiJtMLt1BcBpttYWKvELK3A1F6zLjbuqpUaoow=";
            string notificationHubPath = "vidpack";

            _NotificationHubClient = NotificationHubClient.CreateClientFromConnectionString(connectionString, notificationHubPath);
        }

        public async Task<bool> SendToastNotificationAsync(string toast, string xmlTemplate, string notificationTag)
        {
            string payLoad = String.Format(xmlTemplate, toast); 
            await _NotificationHubClient.SendWindowsNativeNotificationAsync(payLoad, notificationTag);

            return true; 
        }

        public async Task<bool> SendTileNotificationAsync(TileNotification tileUpdate, string xmlTemplate, string notificationTag)
        {
            string payLoad = String.Format(xmlTemplate, tileUpdate.Headline, tileUpdate.Line1, tileUpdate.Line2, tileUpdate.Line3);
            await _NotificationHubClient.SendWindowsNativeNotificationAsync(payLoad, notificationTag);

            return true;
        }


        public async Task<List<NotificationInfo>> LoadNotificationTagAsync()
        {
            string serviceEndpoint = "notification";
            List<NotificationInfo> returnValue = new List<NotificationInfo>();

            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                string jsonResult = await httpClient.GetStringAsync(uri);
                returnValue = Deserialize<List<NotificationInfo>>(jsonResult);
            }

            return returnValue;

        }

        public LocalConfigurationInfo ReadLocalConfiguration()
        {
            LocalConfigurationInfo localConfiguration = new LocalConfigurationInfo()
            {
                BackendUrl = ConfigurationManager.AppSettings.Get("BackendUrl"),
                NotificationHub_ConnectionString = ConfigurationManager.AppSettings.Get("NotificationHub_ConnectionString"),
                NotificationHub_HubPath = ConfigurationManager.AppSettings.Get("NotificationHub_HubPath"),
            };
            return localConfiguration; 
        }

        //********************************************************************************************
        //* JSON Helper
        //********************************************************************************************
        public T Deserialize<T>(string json)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(json);
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(memoryStream);
            }
        }


        
    }
}
