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
    [Guid("015D4056-3B64-4065-BBE8-5579BC8EA98B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerWrapperCom: IPowerWrapper
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern UInt32 CallNtPowerInformation(
            Int32 InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize
        );

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool SetSuspendState(
            bool bHibernate,
            bool bForce,
            bool bWakeupEventsDisabled
        );

        public ulong GetLastSleepTime()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));

            var result = CallNtPowerInformation((int)InformationLevel.LastSleepTime, IntPtr.Zero, 0, status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var sleepTime = (ulong)status.ToInt64();
            Marshal.FreeHGlobal(status);
            return sleepTime;
        }

        public ulong GetLastWakeTime()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));

            var result = CallNtPowerInformation((int)InformationLevel.LastWakeTime, IntPtr.Zero, 0, status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var wakeTime = (ulong)status.ToInt64();
            Marshal.FreeHGlobal(status);
            return wakeTime;
        }

        public SystemBatteryState GetBatteryState()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SystemBatteryState)));

            uint result = CallNtPowerInformation((int)InformationLevel.SystemBatteryState, IntPtr.Zero, 0, status, (UInt32)Marshal.SizeOf(typeof(SystemBatteryState)));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var state = (SystemBatteryState)Marshal.PtrToStructure(status, typeof(SystemBatteryState));
            Marshal.FreeHGlobal(status);
            return state;
        }

        public SystemPowerInformation GetPowerInformation()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SystemPowerInformation)));

            uint result = CallNtPowerInformation((int)InformationLevel.SystemPowerInformation, IntPtr.Zero, 0, status, (UInt32)Marshal.SizeOf(typeof(SystemPowerInformation)));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var state = (SystemPowerInformation)Marshal.PtrToStructure(status, typeof(SystemPowerInformation));
            Marshal.FreeHGlobal(status);
            return state;
        }

        public void ReserveHibernation()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr flagMem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));

            byte reserveFlag = 1;
            Marshal.WriteByte(flagMem, reserveFlag);

            var result = CallNtPowerInformation((int)InformationLevel.SystemReserveHiberFile, flagMem, sizeof(byte), status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }
        }

        public void DeleteHibernation()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr flagMem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));

            byte reserveFlag = 0;
            Marshal.WriteByte(flagMem, reserveFlag);

            var result = CallNtPowerInformation((int)InformationLevel.SystemReserveHiberFile, flagMem, sizeof(byte), status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }
        }

        public void GoToSleep()
        {
            var result = SetSuspendState(true, false, false);
            if (!result)
            {
                throw new Exception("error returned");
            }
        }
    }
}
