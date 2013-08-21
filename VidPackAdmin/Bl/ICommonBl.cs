using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackAdmin.ViewModel;
using VidPackModel;

namespace VidPackAdmin.Bl
{
    public interface ICommonBl
    {
        Task<bool> SendToastNotificationAsync(string toast, string xmlTemplate, string notificationTag  );
        Task<bool> SendTileNotificationAsync(TileNotification tileUpdate, string xmlTemplate, string notificationTag );

        Task<List<NotificationInfo>> LoadNotificationTagAsync();

        LocalConfigurationInfo ReadLocalConfiguration();

    }
}
