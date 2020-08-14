using System.ComponentModel.DataAnnotations;

namespace Moustique.Models.Enums
{
    public enum Pages
    {
        [Display(Name = "Home")]
        Home,
        [Display(Name = "Usługi")]
        Service,
        [Display(Name = "O nas")]
        About,
        [Display(Name = "Cennik")]
        Price,
        [Display(Name = "Kontakt")]
        Contact
    }
}
