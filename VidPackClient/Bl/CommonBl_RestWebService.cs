using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.BL;
using VidPackModel;


namespace VidPackClient.Bl
{
    public class CommonBl_RestWebService : ICommonBl
    {
        //public Uri _webServiceUri = new Uri("http://localhost:19513/api/");
        public Uri _webServiceUri = new Uri("http://vidpack.azurewebsites.net/api/");


        public async Task<Session> LoadActualSession()
        {
            string serviceEndpoint = "actualsession";
            Session returnValue = new Session(); 

            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                JObject jsonResult = JObject.Parse(await httpClient.GetStringAsync(uri));
                returnValue = jsonResult.ToObject<Session>();
            }

            return returnValue;
        }


        public async Task<Session> LoadNextSession()
        {
            string serviceEndpoint = "nextsession";
            Session returnValue = new Session();

            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                JObject jsonResult = JObject.Parse(await httpClient.GetStringAsync(uri));
                returnValue = jsonResult.ToObject<Session>();
            }

            return returnValue;
        }


        public async Task<List<Session>> LoadPastSession()
        {
            string serviceEndpoint = "session";
            List<Session> returnValue = new List<Session>();

            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(string.Format("{0}{1}", _webServiceUri, serviceEndpoint));
                string jsonResult = await httpClient.GetStringAsync(uri);
                returnValue =  Deserialize<List<Session>>(jsonResult);
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
