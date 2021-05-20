﻿using ClassLibrary.Models;
using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Data
{
    public class IOTReceiver
    {
        EventHubClient eventHubClient { get; set; }
        PartitionReceiver eventReceiver { get; set; }

        const string EventHubCompatibleEndPoint = "sb://ihsuproddbres030dednamespace.servicebus.windows.net/";
        const string EventHubCompatiblePath = "iothub-ehub-iotprojekt-11034214-aa0f2b4ca1";
        const string IotHubSasKeyName = "iothubowner";
        const string IotHubSasKey = "Cfy7AdJMKJ5NT4FN1nSZ6YRz5sjllsFXma1G3dIb71c=";


        private Queue<PumpInfo> MainQueue;
        public event EventHandler MessageReceived;


        public IOTReceiver()
        {
            MainQueue = new Queue<PumpInfo>();
            eventHubClient = EventHubClient.CreateFromConnectionString(
                new EventHubsConnectionStringBuilder(new Uri(EventHubCompatibleEndPoint), EventHubCompatiblePath, IotHubSasKeyName, IotHubSasKey).ToString());
        }

        public async void StartReceieveMessagesFromDevice()
        {
            Console.WriteLine("Initializing receiver...");
            //var runtimeInfo = await eventHubClient.GetRuntimeInformationAsync();
            //var partitions = runtimeInfo.PartitionIds;

            //Partitions på IOT huben er der to. Det er bare hvilken del, på den måde kan man have en listener på hver, og have kaffemaskiner på den ene,
            //og ngoet på den anden
            if (eventReceiver == null)
            {
                eventReceiver = eventHubClient.CreateReceiver("$Default", "0", EventPosition.FromEnqueuedTime(DateTime.Now));
            }

            while (true)
            {
                IEnumerable<EventData> events = await eventReceiver.ReceiveAsync(100);


                //List<PumpInfo> pumpInfos = new List<PumpInfo>();
                foreach (EventData eventData in events)
                {
                    string data = Encoding.UTF8.GetString(eventData.Body.Array);
                    //Console.WriteLine("Message received on partition {0}:", partitions[0]);
                    Console.WriteLine("  {0}:", data);
                    //pumpInfos.Add();
                    MainQueue.Enqueue(JsonSerializer.Deserialize<PumpInfo>(data));
                }


                MessageReceived?.Invoke(this, EventArgs.Empty);
            }
        }

        public PumpInfo ConsumeMessage()
        {
            return MainQueue.Dequeue();
        }









    }
}
