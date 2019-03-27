using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var wrapper = new PowerWrapper();
            try
            {
                Console.WriteLine("last sleep time is:");
                var result = wrapper.GetLastSleepTime();
                Console.WriteLine(result);

                Console.WriteLine("last wake time is:");
                result = wrapper.GetLastWakeTime();
                Console.WriteLine(result);

                Console.WriteLine("some info about battery: Ac online?");
                var doesExist = wrapper.GetBatteryState().AcOnLine;
                Console.WriteLine(doesExist);

                Console.WriteLine("Idleness");
                var smth = wrapper.GetPowerInformation().Idleness;
                Console.WriteLine(smth);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
