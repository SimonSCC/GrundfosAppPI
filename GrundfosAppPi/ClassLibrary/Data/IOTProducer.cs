using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.Azure.Devices.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.EventHubs;
using System.Threading;

namespace ClassLibrary.Data
{
    public class IOTProducer
    {
        private static DeviceClient deviceClient;

        public IOTProducer(string connectionString)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

        }

     
        public async void Send(PumpInfo pumpInfo)
        {
            Message message = new Message(Encoding.ASCII.GetBytes(pumpInfo.ToJson()));


            //message.Properties.Add("pumpEvent", "true");
            await deviceClient.SendEventAsync(message);
            Console.WriteLine("Sending Message {0}", pumpInfo.ToJson());



        }

        //public async Task<PumpInfo> Receive()
        //{


        //    Message message = await deviceClient.ReceiveAsync();

        //    if (message != null)
        //    {
        //        string messageString = Encoding.ASCII.GetString(message.GetBytes());
        //        Console.WriteLine("Received Message {0}", messageString);
        //        await deviceClient.CompleteAsync(message);
        //        return JsonSerializer.Deserialize<PumpInfo>(messageString);
        //    }

        //    return null;
        //}


    }
}
