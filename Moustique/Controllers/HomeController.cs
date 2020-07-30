using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moustique.Models.ViewModels;
using Moustique.Services;
using Moustique.Services.Interfaces;

namespace Moustique.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IIPAddressService _iPAddressService;

        public HomeController(IEmailService emailService, IIPAddressService iPAddressService)
        {
            _emailService = emailService;
            _iPAddressService = iPAddressService;
        }

        public IActionResult Index()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var IPinfo = _iPAddressService.ShowInfo(remoteIpAddress.ToString());
            if (IPinfo.ip == null)
            {
                var emailNoIp = new EmailViewModel
                {
                    IsHtml = true,
                    Subject = "Wiadomość ze strony PNK - wejście na stronę",
                    Body = $"Wejscie na stronę {Environment.NewLine}IP: {remoteIpAddress}"
                };
                _emailService.SendEmailAsync(emailNoIp);
                return View();
            }
            
            var email = new EmailViewModel
            {
                IsHtml = true,
                Subject = "Wiadomość ze strony PNK - wejście na stronę",
                Body = $"Wejscie na stronę IP: {remoteIpAddress}<br />Miasto: {IPinfo.city}<br />Państwo: {IPinfo.country_name}<br />Lokalizacja: {IPinfo.location.capital} <br />Kod: {IPinfo.zip}"
            };
            _emailService.SendEmailAsync(email);
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail([FromBody] EmailViewModel email)
        {
            if (email != null && !string.IsNullOrEmpty(email.Name) && !string.IsNullOrWhiteSpace(email.Email) && email.Phone != 0 && !string.IsNullOrWhiteSpace(email.Message))
            {
                email.IsHtml = true;
                email.Subject = "Wiadomość ze strony Polowanie Na Komary - formularz kontaktowy";
                email.Body = $"<h1>Od: {email.Name}</h1>{Environment.NewLine}<h2>E-mail: {email.Email} </h2>{Environment.NewLine}<div>Telefon: {email.Phone} </h2>{Environment.NewLine}<div>Wiadomość: {email.Message}</div>";


                //info = "Wiadomość została wysłana";
                return Json(await _emailService.SendEmailAsync(email));
            }
            //info = "Wiadomość nie została wysłana";
            return Json(false);

        }
    }
}
