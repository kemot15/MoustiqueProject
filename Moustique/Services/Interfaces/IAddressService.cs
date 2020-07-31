using static Moustique.Models.ViewModels.InfoAddress;

namespace Moustique.Services.Interfaces
{
    public interface IAddressService
    {
        public Rootobject ShowInfo(string IP);
    }
}
