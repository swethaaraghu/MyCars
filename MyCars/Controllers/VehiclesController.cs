using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyCars.EntityData;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;

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

        [HttpPost(ConstantsData.GetAllVehicle)]
        public List<Vehicle> GetAllVehicle([FromBody]string count)
        {
            StreamReader r = new StreamReader("EntityData/VehicleSampleData.json");

            try
            {
                int rowCount = int.Parse(count);

                string jsonString = r.ReadToEnd();

                List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);

                if ((lstVehicles.Count - rowCount) < 10 && (lstVehicles.Count - rowCount) > 0)
                    return lstVehicles.GetRange(rowCount, (lstVehicles.Count - rowCount));
                else if ((lstVehicles.Count - rowCount) < 0)
                    return null;
                return lstVehicles.GetRange(rowCount, 10);
            }
            catch
            {
                return null;
            }
            finally
            {
                r.Close();
            }
        }

        [HttpGet(ConstantsData.GetMapData)]
        public string GetMapData()
        {
            StreamReader r = new StreamReader("EntityData/MapData.json");
            try
            {
                string jsonString = r.ReadToEnd();

                return jsonString;
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                r.Close();
            }
        }

        [HttpPost(ConstantsData.GetVehicleByVin)]
        public List<Vehicle> GetVehicleByVin([FromBody]string searchText)
        {
            StreamReader r = new StreamReader("EntityData/VehicleSampleData.json");
            try
            {
                string jsonString = r.ReadToEnd();

                List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);

                lstVehicles = lstVehicles.Where(o =>(  o.VinNumber.ToLower()
                                                            .Contains(searchText.ToLower()) || o.DriverName.ToLower()
                                                                .Contains(searchText.ToLower()) || 
                                                                  o.LicensePlateNumber.ToLower()
                                                                    .Contains(searchText.ToLower())))
                                                                        .Distinct().ToList();
                return lstVehicles;
            }
            catch 
            { 
                return null; 
            }
            finally
            {
                r.Close();
            }
        }

        [HttpPatch(ConstantsData.SaveEditedVehicle)]
        public int SaveEditedVehicle([FromBody] string jsonData)
        {
            StreamReader r = new StreamReader("EntityData/VehicleSampleData.json");
            TextWriter writer = null;

            try
            { 
                Vehicle vehicles = JsonConvert.DeserializeObject<Vehicle>(jsonData);

                string jsonString = r.ReadToEnd();
                List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);
                r.Close();
                foreach (var item in lstVehicles.Where(w => w.VinNumber == vehicles.VinNumber))
                {
                    item.Office = vehicles.Office;
                    item.LicensePlateNumber = vehicles.LicensePlateNumber;
                    item.DriverName = vehicles.DriverName;
                }
           
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(lstVehicles, Newtonsoft.Json.Formatting.Indented);
                
                using (writer = new StreamWriter("EntityData/VehicleSampleData.json", append: false))
                {
                    writer.WriteLine(output);
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                //r.Close();

                if(writer != null)
                    writer.Close();

            }
        }

        [HttpPatch(ConstantsData.AddVehicle)]
        public int AddVehicle([FromBody] string jsonData)
        {
            StreamReader r = new StreamReader("EntityData/VehicleSampleData.json");

            TextWriter writer = null;

            try
            {
                Vehicle vehicles = JsonConvert.DeserializeObject<Vehicle>(jsonData);

                string jsonString = r.ReadToEnd();

                List<Vehicle> lstVehicles = JsonConvert
                                    .DeserializeObject<List<Vehicle>>(jsonString);
                r.Close();

                vehicles.CustomerName = "Raghupathy";  

                vehicles.Status = new VehicleStatus();
                
                lstVehicles.Add(vehicles);

                string output = Newtonsoft.Json.JsonConvert
                                    .SerializeObject(lstVehicles, 
                                        Newtonsoft.Json.Formatting.Indented);

                using (writer = new StreamWriter("EntityData/VehicleSampleData.json", append: false))
                {
                    writer.WriteLine(output);
                }

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                //r.Close();

                if (writer != null)
                    writer.Close();

            }
        }

    }
}
