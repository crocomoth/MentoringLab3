using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SystemPowerInformation
    {
        internal uint MaxIdlenessAllowed;
        internal uint Idleness;
        internal uint TimeRemaining;
        internal byte CoolingMode;
    }
}
