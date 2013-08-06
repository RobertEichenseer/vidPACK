using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackModel;

namespace VidPackClient.Bl
{
    public interface ICommonBl
    {
        void SetConfigPara(ClientConfig clientConfig);
        Task<Session> LoadActualSession();
        Task<Session> LoadNextSession();
        Task<List<Session>> LoadPastSession();

        Task<List<NotificationInfo>> LoadNotifications();
    }
}
