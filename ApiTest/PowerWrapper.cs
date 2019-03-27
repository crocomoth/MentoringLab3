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

        internal ulong GetLastSleepTime()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));

            var result = CallNtPowerInformation((int)InformationLevel.LastSleepTime, IntPtr.Zero, 0, out status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var sleepTime = (ulong) status.ToInt64();
            Marshal.FreeHGlobal(status);
            return sleepTime;
        }

        internal ulong GetLastWakeTime()
        {
            IntPtr status = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(ulong)));

            var result = CallNtPowerInformation((int)InformationLevel.LastWakeTime, IntPtr.Zero, 0, out status, sizeof(ulong));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            var wakeTime = (ulong)status.ToInt64();
            Marshal.FreeHGlobal(status);
            return wakeTime;
        }

        internal SystemBatteryState GetBatteryState()
        {
            IntPtr status = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemBatteryState)));

            var result = CallNtPowerInformation((int)InformationLevel.SystemBatteryState, IntPtr.Zero, 0, out status, (UInt32)Marshal.SizeOf(typeof(SystemBatteryState)));
            if (result != 0)
            {
                throw new Exception("not 0");
            }

            SystemBatteryState state = new SystemBatteryState();
            Marshal.PtrToStructure<SystemBatteryState>(status, state);
            return state;
        }
    }
}
