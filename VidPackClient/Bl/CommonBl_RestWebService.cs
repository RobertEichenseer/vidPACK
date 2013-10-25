using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.Bl;
using VidPackClient.Common;
using VidPackModel;
using Windows.UI.Xaml.Media.Imaging;


namespace VidPackClient.Bl
{
    public class CommonBl_RestWebService : ICommonBl
    {
        public Uri _webServiceUri = new Uri("http://localhost:19513/api/");
        //public Uri _webServiceUri;  //= new Uri("http://vidpack.azurewebsites.net/api/");

        public void SetConfigPara(ClientConfig clientConfig)
        {
            _webServiceUri = new Uri(clientConfig.WebServiceUrl); 
        }

        public async Task<Session> LoadActualSession()
        {
            string serviceEndpoint = "actualsession";
            Session returnValue = new Session();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                    JObject jsonResult = JObject.Parse(await httpClient.GetStringAsync(uri));
                    returnValue = jsonResult.ToObject<Session>();
                }
            }
            catch (HttpRequestException exception)
            {
                ErrorHandler.AddError(exception);
            }

            return returnValue;
        }


        public async Task<Session> LoadNextSession()
        {
            string serviceEndpoint = "nextsession";
            Session returnValue = new Session();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                    JObject jsonResult = JObject.Parse(await httpClient.GetStringAsync(uri));
                    returnValue = jsonResult.ToObject<Session>();
                }
            }
            catch (HttpRequestException exception)
            {
                ErrorHandler.AddError(exception);
            }

            return returnValue;
        }


        public async Task<List<Session>> LoadPastSession()
        {
            string serviceEndpoint = "session";
            List<Session> returnValue = new List<Session>();

            try { 
                using (HttpClient httpClient = new HttpClient())
                {
                    Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                    string jsonResult = await httpClient.GetStringAsync(uri);
                    returnValue =  Deserialize<List<Session>>(jsonResult);
                }
            }
            catch (HttpRequestException exception)
            {
                ErrorHandler.AddError(exception);
            }

            return returnValue;
         }

        public async Task<List<NotificationInfo>> LoadNotifications()
        {
            string serviceEndpoint = "notification";
            List<NotificationInfo> returnValue = new List<NotificationInfo>();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                    string jsonResult = await httpClient.GetStringAsync(uri);
                    returnValue = Deserialize<List<NotificationInfo>>(jsonResult);
                }
            }
            catch (HttpRequestException exception)
            {
                ErrorHandler.AddError(exception);
            }

            return returnValue;
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
