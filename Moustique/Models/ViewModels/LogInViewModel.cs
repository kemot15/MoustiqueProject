using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Models.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }
    }
}
