using System;
using System.Collections.Generic;
using System.Linq;
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

        public HomeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail([FromBody] EmailViewModel email)
        {
            if (email != null && !string.IsNullOrEmpty(email.Name) && !string.IsNullOrWhiteSpace(email.Email) && email.Phone != 0 && !string.IsNullOrWhiteSpace(email.Message))
            {
                email.IsHtml = true;
                email.Subject = "Wiadomość ze strony Polowanie Na Komary - formularz kontaktowy";
                email.Body = $"<h1>Od: {email.Name}</h1>{Environment.NewLine}<h2>E-mail: {email.Email} </h2>{Environment.NewLine}<div>Wiadomość: {email.Message}</div>";


                //info = "Wiadomość została wysłana";
                return Json(await _emailService.SendEmailAsync(email));
            }
            //info = "Wiadomość nie została wysłana";
            return Json(false);

        }
    }
}
