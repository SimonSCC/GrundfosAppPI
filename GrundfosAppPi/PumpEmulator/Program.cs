using ClassLibrary.Data;
using ClassLibrary.Models;
using ClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace PumpEmulator
{
    class Program
    {
        IOTProducer iOTHub = new IOTProducer("HostName=IotProjekt.azure-devices.net;DeviceId=Pumpe1;SharedAccessKey=ZicGAUZfLLE4CcxTc4oCqz0gpm6i3ZzpuP+NG2oXxCs=");
        //EmailService _emailService = new EmailService("smtp.gmail.com", 587, new NetworkCredential(CredentialsRemoved), true);

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.MainFlow();
        }

        private void MainFlow()
        {
            Pump thisPump = new Pump("ALPHA3 Model B");

            while (thisPump.TurnedOn)
            {
               PumpInfo pumpData = thisPump.GetValues();

                Console.WriteLine(pumpData);
                SendToIoTHub(pumpData);
                Thread.Sleep(5000);
            }
        }

        private void SendToIoTHub(PumpInfo pumpInfo)
        {
            List<DangerReading> potentialDangerReadings = pumpInfo.DoesContainDangerValues();
            if (potentialDangerReadings.Count != 0)
            {
                foreach (DangerReading reading in potentialDangerReadings)
                {
                    SendEmail(reading);
                }
            } 
            iOTHub.Send(pumpInfo);
        }

        private void SendEmail(DangerReading potentialDangerReading)
        {
            //_emailService.Send("simonsondergaardchristiansen@gmail.com", "Danger from " + potentialDangerReading.PumpName, potentialDangerReading.ToString());
        }
    }
}
