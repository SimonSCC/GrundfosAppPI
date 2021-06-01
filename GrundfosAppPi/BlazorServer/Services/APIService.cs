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
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44389/");
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
