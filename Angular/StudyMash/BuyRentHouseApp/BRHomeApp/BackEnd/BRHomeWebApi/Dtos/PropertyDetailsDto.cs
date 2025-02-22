using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRHomeWebApi.Dtos
{
    public class PropertyDetailsDto : PropertyListDto
    {
        public double CarpetArea { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public int FloorNo { get; set; }
        public int TotalFloors { get; set; } 
        public string MainEntrance { get; set; }
        public int Age { get; set; } = 0;
        public int Security { get; set; }  = 0;
        public bool Gated { get; set; }
        public int Maintenance { get; set; }  = 0;
        public string Description { get; set; }
         public ICollection<PhotoDto>  Photos { get; set; }
    }
}