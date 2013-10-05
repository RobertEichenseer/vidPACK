using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackAdmin.Bl;

namespace VidPackAdmin.ViewModel
{
    class VideoViewModel : BaseViewModel
    {
        ICommonBl _bl;
        MainAdminViewModel _mainAdminViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bl"> Business Logic</param>
        /// <param name="mainAdminViewModel"></param>
        public VideoViewModel(ICommonBl bl, MainAdminViewModel mainAdminViewModel)
        {
            _bl = bl;
            _mainAdminViewModel = mainAdminViewModel;
        }

    }
}
