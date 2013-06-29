using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackModel;

namespace VidPackClient.BL
{
    public interface ICommonBl
    {
        void SetConfigPara(object configPara);
        Task<Session> LoadActualSession();
        Task<Session> LoadNextSession();
        Task<List<Session>> LoadPastSession();
    }
}
