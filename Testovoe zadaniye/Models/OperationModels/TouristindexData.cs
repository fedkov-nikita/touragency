using System.Collections.Generic;
using Testovoe_zadaniye.Models.Entities;

namespace Testovoe_zadaniye.Models.OperationModels
{
    public class TouristindexData
    {
        public IEnumerable<Tourist> Tourists { get; set; }
        public IEnumerable<Guide> Guides { get; set; }
        public IEnumerable<Tour> Tours { get; set; }
    }
}
