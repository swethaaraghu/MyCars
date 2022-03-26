using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyCars.EntityData;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace MyCars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {            
            return "success";
        }

        [HttpGet(ConstantsData.GetAllVehicle)]
        public List<Vehicle> GetAllVehicle()
        {
            StreamReader r = new StreamReader("EntityData/VehicleSampleData.json");
            string jsonString = r.ReadToEnd();
            List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);
            return lstVehicles;
        }

        [HttpGet(ConstantsData.GetMapData)]
        public string GetMapData()
        {
            StreamReader r = new StreamReader("EntityData/MapData.json");
            string jsonString = r.ReadToEnd();            
            return jsonString;
        }

        [HttpPost(ConstantsData.GetVehicleByVin)]
        public List<Vehicle> GetVehicleByVin([FromBody]string vinNumber)
        {
            StreamReader r = new StreamReader("EntityData/VehicleSampleData.json");           
            string jsonString = r.ReadToEnd();
            List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);
            lstVehicles = lstVehicles.Where(o => o.VinNumber.ToLower().Contains(vinNumber.ToLower())).ToList();
            return lstVehicles;
        }

    }
}
