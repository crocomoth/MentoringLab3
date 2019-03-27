using PowerWrapperCom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerWrapperCom
{
    [ComVisible(true)]
    [Guid("D0738B98-4745-4B76-9C7D-4D728D6A5AFA")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerWrapper
    {
        ulong GetLastSleepTime();
        ulong GetLastWakeTime();
        SystemBatteryState GetBatteryState();
        SystemPowerInformation GetPowerInformation();
        void ReserveHibernation();
        void DeleteHibernation();
        void GoToSleep();
    }
}
