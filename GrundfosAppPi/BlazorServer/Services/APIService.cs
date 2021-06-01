using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace BlazorServer.Services
{
    public class APIService
    {
        private readonly HttpClient _httpClient;
        public EventHandler PumpInfoReceived;
        public APIService()
        {
            //In the API change where it is hosted in launchsettings.json
            //For Local Environment it needs to be
            //"applicationUrl": "https://localhost:5001;http://localhost:5000",

            //For Rasberry PI it need to be:
            //"applicationUrl": "https://192.168.0.2:5001;http://192.168.0.2:5000",

            //Also make sure to run the app as BlazorServer instead of IIS Express

            //You can also write:
            //"applicationUrl": "https://*:5001;http://*:5000",




            //For API running on Rasberry PI:

            //HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //// Pass the handler to httpclient(from you are calling api)
            //_httpClient = new HttpClient(clientHandler);
            //_httpClient.BaseAddress = new Uri("https://80.167.81.52:5001/");

            //



            ////For API running on localhost

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44389/");

            ////


            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async void StartLoop()
        {
            while (true)
            {
                PumpInfo info = await GetLatestValue();
                if (info != null)
                {
                    PumpInfoReceived?.Invoke(info, EventArgs.Empty);
                }
            }


        }
        public async Task<PumpInfo> GetLatestValue()
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync("IoTDistributor").ConfigureAwait(false)) //Look up hvorfor dette viker
            {
                if (response.IsSuccessStatusCode)
                {
                    string contentAsString = await response.Content.ReadAsStringAsync();
                    //PumpInfo result = JsonSerializer.Deserialize<PumpInfo>(contentAsString);
                    PumpInfo result = JsonConvert.DeserializeObject<PumpInfo>(contentAsString);
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
        }

    }
}
