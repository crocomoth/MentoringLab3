using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ApiTest.Models;

namespace ApiTest
{
    internal class PowerWrapper
    {
        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern UInt32 CallNtPowerInformation(
            Int32 InformationLevel,
            IntPtr lpInputBuffer,
            UInt32 nInputBufferSize,
            out IntPtr lpOutputBuffer,
            UInt32 nOutputBufferSize
        );

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern bool SetSuspendState(
            bool bHibernate,
            bool bForce,
            bool bWakeupEventsDisabled
        );

        internal ulong GetLastSleepTime()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr hook = status;

            var result = CallNtPowerInformation((int)InformationLevel.LastSleepTime, IntPtr.Zero, 0, out status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var sleepTime = (ulong) status.ToInt64();
            Marshal.FreeHGlobal(hook);
            return sleepTime;
        }

        internal ulong GetLastWakeTime()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr hook = status;

            var result = CallNtPowerInformation((int)InformationLevel.LastWakeTime, IntPtr.Zero, 0, out status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var wakeTime = (ulong)status.ToInt64();
            Marshal.FreeHGlobal(hook);
            return wakeTime;
        }

        internal SystemBatteryState GetBatteryState()
        {
            IntPtr status = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemBatteryState)));
            IntPtr hook = status;

            var result = CallNtPowerInformation((int)InformationLevel.SystemBatteryState, IntPtr.Zero, 0, out status, (UInt32)Marshal.SizeOf(typeof(SystemBatteryState)));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var state = (SystemBatteryState)Marshal.PtrToStructure(hook, typeof(SystemBatteryState));
            Marshal.FreeHGlobal(hook);
            return state;
        }

        internal SystemPowerInformation GetPowerInformation()
        {
            IntPtr status = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemPowerInformation)));
            IntPtr hook = status;

            var result = CallNtPowerInformation((int)InformationLevel.SystemPowerInformation, IntPtr.Zero, 0, out status, (UInt32)Marshal.SizeOf(typeof(SystemPowerInformation)));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var state = (SystemPowerInformation)Marshal.PtrToStructure(hook, typeof(SystemPowerInformation));
            Marshal.FreeHGlobal(hook);
            return state;
        }

        internal void ReserveHibernation()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr flagMem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));

            byte reserveFlag = 1;
            Marshal.WriteByte(flagMem, reserveFlag);

            var result = CallNtPowerInformation((int)InformationLevel.SystemReserveHiberFile, flagMem, sizeof(byte), out status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }
        }

        internal void DeleteHibernation()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));
            IntPtr flagMem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)));

            byte reserveFlag = 0;
            Marshal.WriteByte(flagMem, reserveFlag);

            var result = CallNtPowerInformation((int)InformationLevel.SystemReserveHiberFile, flagMem, sizeof(byte), out status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }
        }

        internal void GoToSleep()
        {
            var result = SetSuspendState(true, false, false);
            if (!result)
            {
                throw new Exception("error returned");
            }
        }
    }
}
