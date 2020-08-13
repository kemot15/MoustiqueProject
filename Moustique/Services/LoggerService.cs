using Moustique.Context;
using Moustique.Models.Db;
using Moustique.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moustique.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly MoustiqueContext _context;

        public LoggerService(MoustiqueContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveIpAddressAsync (Statistics statistics)
        {
            await _context.Statistics.AddAsync(statistics);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
