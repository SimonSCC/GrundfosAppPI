using ClassLibrary.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //builder.Services.AddSingleton<IOTHub>(new IOTHub("HostName=IotProjekt.azure-devices.net;DeviceId=Pumpe1;SharedAccessKey=ZicGAUZfLLE4CcxTc4oCqz0gpm6i3ZzpuP+NG2oXxCs="));

            await builder.Build().RunAsync();
        }
    }
}
