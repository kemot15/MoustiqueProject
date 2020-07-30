using Microsoft.Extensions.Configuration;
using Moustique.Models.ViewModels;
using Moustique.Services.Interfaces;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Moustique.Services
{
    public class IPAddressService : IIPAddressService
    {
        public Models.ViewModels.IPAddress.Rootobject ShowInfo (string IP)
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
                //ViewBag.Info = "Miejscowość nie została odnaleziona";
                //return Index("Wrocław");
            }
            var result = (new JavaScriptSerializer()).Deserialize<Models.ViewModels.IPAddress.Rootobject>(json);
            return result;
        }

    }
}
