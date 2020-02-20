using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testovoe_zadaniye.DataBase;
using Testovoe_zadaniye.Models;

namespace Testovoe_zadaniye.Models
{
    public class TouristindexData
    {
        public IEnumerable<Tourist> Tourists { get; set; }
        public IEnumerable<Guide> Guides { get; set; }
        public IEnumerable<Tour> Tours { get; set; }
    }
}
