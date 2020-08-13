using Moustique.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Models.Db
{
    public class Statistics
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string AddressIP { get; set; }

        public Pages Page { get; set; } 
    }
}
