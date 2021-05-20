using ClassLibrary.Data;
using ClassLibrary.Models;
using System;
using System.Threading;

namespace PumpEmulator
{
    class Program
    {
        static IOTHub iOTHub = new IOTHub("HostName=IotProjekt.azure-devices.net;DeviceId=Pumpe1;SharedAccessKey=ZicGAUZfLLE4CcxTc4oCqz0gpm6i3ZzpuP+NG2oXxCs=");

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

                SendToIoTHub(pumpData);
                Thread.Sleep(15000);
            }
        }

        private async static void SendToIoTHub(PumpInfo pumpInfo)
        {
            iOTHub.Send(pumpInfo);
            await iOTHub.Receive();
        }
    }
}
