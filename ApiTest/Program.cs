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

                Console.WriteLine("some info about battery: Battery exists?");
                var doesExist = wrapper.GetBatteryState();
                Console.WriteLine(doesExist);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
