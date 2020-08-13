using Moustique.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Services.Interfaces
{
    public interface ILoggerService
    {
        Task<bool> SaveIpAddressAsync(Statistics statistics);
        Task<IList<Statistics>> GetVisitsStatisticsAsync();
    }
}
