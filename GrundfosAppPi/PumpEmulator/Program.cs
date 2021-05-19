using ClassLibrary.Models;
using System;
using System.Threading;

namespace PumpEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            MainFlow();

        }

        private static void MainFlow()
        {
            Pump thisPump = new Pump("ALPHA3 Model B");

            while (thisPump.TurnedOn)
            {
               PumpInfo pumpData = thisPump.GetValues();

                Console.WriteLine(pumpData);


                SendToIoTHub();
                Thread.Sleep(15000);
            }
        }

        private static void SendToIoTHub()
        {
            throw new NotImplementedException();
        }
    }
}
