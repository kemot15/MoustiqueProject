using Moustique.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailViewModel model);
    }
}
