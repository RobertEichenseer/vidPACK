using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidPackClient.BL;
using VidPackModel;

namespace VidPackClient.ViewModel
{
    class SessionDetailInputPara
    {
        public ICommonBl Bl { get; set; }
        public Session SelectedSession { get; set; }

    }
}
