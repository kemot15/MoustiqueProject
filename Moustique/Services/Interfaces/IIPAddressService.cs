using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Services.Interfaces
{
    public interface IIPAddressService
    {
        public Models.ViewModels.IPAddress.Rootobject ShowInfo(string IP);
    }
}
