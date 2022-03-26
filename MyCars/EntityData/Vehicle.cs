using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCars.EntityData
{
    
    public class Vehicle
    {
        public string VinNumber { get; set; }
        public string LicensePlateNumber { get; set; }
        public string DriverName { get; set; }
        public string VehicleModel { get; set; }
        public string CustomerName { get; set; }
        public string VehicleId { get; set; }
        public string Office { get; set; }
        public VehicleStatus Status { get; set; }


    }
}
