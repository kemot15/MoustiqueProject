using Microsoft.Extensions.Configuration;
using Moustique.Services.Interfaces;
using Nancy.Json;
using System;
using static Moustique.Models.ViewModels.InfoAddress;

using System.Net;
using Moustique.Models.ViewModels;

namespace Moustique.Services
{
    public class AddressService : IAddressService
    {
        public Rootobject ShowInfo (string IP)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", true);
            var config = configurationBuilder.Build();

            string appid = config.GetSection("IPAddress:Appid").Value;
                                                                         
            string url = string.Format($"http://api.ipapi.com/{IP}?access_key={appid}&lenguage=pl");
            var json = "";
            var client = new WebClient();
            try
            {
                json = client.DownloadString(url);
            }
            catch (Exception)
            {
                var alterResult = new Rootobject() { ip = null };
                return alterResult;
            }
            var result = (new JavaScriptSerializer()).Deserialize<InfoAddress.Rootobject>(json);
                        
            return result;
        }

    }
}
