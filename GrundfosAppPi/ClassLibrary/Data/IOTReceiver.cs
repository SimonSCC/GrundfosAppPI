using ClassLibrary.Models;
using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
            CreateEventHubClient();
        }


        void CreateEventHubClient()
        {
            eventHubClient = EventHubClient.CreateFromConnectionString(
               new EventHubsConnectionStringBuilder(new Uri(EventHubCompatibleEndPoint), EventHubCompatiblePath, IotHubSasKeyName, IotHubSasKey).ToString());
        }

        public async void StartReceieveMessagesFromDevice()
        {
            Console.WriteLine("Initializing receiver...");

            try
            {
                //Partitions på IOT huben er der to. Det er bare hvilken del, på den måde kan man have en listener på hver, og have kaffemaskiner på den ene,
                //og ngoet på den anden
                if (eventReceiver == null)
                {
                    eventReceiver = eventHubClient.CreateReceiver("$Default", "0", EventPosition.FromEnqueuedTime(DateTime.Now));
                }

                while (true)
                {
                    IEnumerable<EventData> events = await eventReceiver.ReceiveAsync(100);

                    foreach (EventData eventData in events)
                    {
                        string data = Encoding.UTF8.GetString(eventData.Body.Array);
                        Console.WriteLine("  {0}:", data);
                        MainQueue.Enqueue(JsonSerializer.Deserialize<PumpInfo>(data));
                    }

                    MessageReceived?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                eventHubClient.Close();
                CreateEventHubClient();
                StartReceieveMessagesFromDevice();
            }
        }

        public PumpInfo ConsumeMessage()
        {
            return MainQueue.Dequeue();
        }
    }
}
