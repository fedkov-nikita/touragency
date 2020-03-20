using System.Collections.Generic;


namespace Testovoe_zadaniye.Models
{
    public class TouristindexData
    {
        public IEnumerable<Tourist> Tourists { get; set; }
        public IEnumerable<Guide> Guides { get; set; }
        public IEnumerable<Tour> Tours { get; set; }
    }
}
