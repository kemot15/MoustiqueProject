﻿using Microsoft.Extensions.Configuration;
using Moustique.Models.ViewModels;
using Moustique.Services.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Moustique.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(EmailViewModel model)
        {
            try
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json", true);
                var config = configurationBuilder.Build();
                var emailFrom = config.GetSection("EmailConfig:EmailFrom").Value;
                var pass = config.GetSection("EmailConfig:Pass").Value;
                var emailTo = config.GetSection("EmailConfig:EmailTo").Value;
                var host = config.GetSection("EmailConfig:Host").Value;
                var port = config.GetSection("EmailConfig:Port").Value;


                MailMessage message = new MailMessage
                {
                    From = new MailAddress(emailFrom),
                    Subject = model.Subject,
                    Body = model.Body,
                    IsBodyHtml = model.IsHtml

                };


                //dpdajemy zalacznik do maila
                if (model.PathAttachment != null)
                {
                    var attachement = new Attachment(model.PathAttachment);
                    message.Attachments.Add(attachement);
                }

                message.To.Add(model.To ?? emailTo);

                SmtpClient client = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailFrom, pass),
                    Host = host,
                    Port = int.Parse(port),
                    EnableSsl = true,
                    Timeout = 5000,

                };

                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
