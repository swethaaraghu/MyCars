using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCars.EntityData
{
    public class VehicleStatus
    {
        public bool Ignition { get; set; }
        public int Speed { get; set; }
        public VehicleLocation Location { get; set; }
    }
}
