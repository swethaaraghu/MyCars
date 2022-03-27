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
        public List<Vehicle> GetAllVehicle([FromBody]string jsonData)
        {
            StreamReader r = null;

            try
            {
                r = new StreamReader("EntityData/VehicleSampleData.json");

                Vehicle vehicles = JsonConvert.DeserializeObject<Vehicle>(jsonData);

                int rowCount = vehicles.Count;

                string jsonString = r.ReadToEnd();

                List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);

                lstVehicles = lstVehicles.Where(l =>l.CustomerName.ToLower() == 
                                                        vehicles.CustomerName.ToLower()).ToList();

                if ((lstVehicles.Count - rowCount) < 10 && (lstVehicles.Count - rowCount) > 0)
                {
                    return lstVehicles.GetRange(rowCount, (lstVehicles.Count - rowCount));
                }
                else if ((lstVehicles.Count - rowCount) <= 0)
                {
                    return null;
                }

                return lstVehicles.GetRange(rowCount, 10);
            }
            catch
            {
                return null;
            }
            finally
            {
                if(r!=null)
                    r.Close();
            }
        }

        [HttpPost(ConstantsData.GetMapData)]
        public string GetMapData([FromBody]string customerName)
        {
            StreamReader r = null;
            try
            {
                r = new StreamReader("EntityData/MapData.json");

                string jsonString = r.ReadToEnd();

                return jsonString;
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if(r!=null)
                    r.Close();
            }
        }

        [HttpPost(ConstantsData.GetVehicleByVin)]
        public List<Vehicle> GetVehicleByVin([FromBody]string jsonData)
        {
            StreamReader r = null;
            try
            {
                r = new StreamReader("EntityData/VehicleSampleData.json");

                Vehicle vehicles = JsonConvert.DeserializeObject<Vehicle>(jsonData);

                string searchText = vehicles.SearchText;

                string jsonString = r.ReadToEnd();

                List<Vehicle> lstVehicles = JsonConvert.DeserializeObject<List<Vehicle>>(jsonString);

                lstVehicles = lstVehicles.Where(o =>((o.CustomerName.ToLower() == vehicles.CustomerName.ToLower()) 
                                                          && (o.VinNumber.ToLower()
                                                            .Contains(searchText.ToLower()) || 
                                                              o.DriverName.ToLower()
                                                                .Contains(searchText.ToLower()) || 
                                                                  o.LicensePlateNumber.ToLower()
                                                                    .Contains(searchText.ToLower()))))
                                                                        .Distinct().ToList();
                return lstVehicles;
            }
            catch 
            { 
                return null; 
            }
            finally
            {
                if (r != null)
                    r.Close();
            }
        }

        [HttpPatch(ConstantsData.SaveEditedVehicle)]
        public int SaveEditedVehicle([FromBody] string jsonData)
        {
            StreamReader r = null;
            TextWriter writer = null;

            try
            {
                r = new StreamReader("EntityData/VehicleSampleData.json");

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
            catch
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
            StreamReader r = null;

            TextWriter writer = null;

            try
            {
                r = new StreamReader("EntityData/VehicleSampleData.json");

                Vehicle vehicles = JsonConvert.DeserializeObject<Vehicle>(jsonData);

                string jsonString = r.ReadToEnd();

                List<Vehicle> lstVehicles = JsonConvert
                                    .DeserializeObject<List<Vehicle>>(jsonString);
                r.Close();

                //vehicles.CustomerName = "Raghupathy";  

                int checkExists = lstVehicles.Where(s => s.VinNumber.ToLower() ==
                                                        vehicles.VinNumber.ToLower()).ToList().Count;
                if (checkExists != 0)
                    return 2;

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
            catch
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

        [HttpPost(ConstantsData.AuthenticateCustomer)]
        public int AuthenticateCustomer([FromBody] string jsonData)
        {
            StreamReader reader = null ;
            try
            {
                reader = new StreamReader("EntityData/CustomerLoginData.json");

                Customer cust = JsonConvert.DeserializeObject<Customer>(jsonData);

                string jsonString = reader.ReadToEnd();

                List<Customer> lstCustomer = JsonConvert
                                    .DeserializeObject<List<Customer>>(jsonString);

                lstCustomer = lstCustomer.Where(o => o.CustomerName.ToLower() == cust.CustomerName.ToLower() &&
                                                        o.Password == cust.Password).ToList();

                if (lstCustomer.Count != 0)
                    return 1;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
            finally
            {
               if(reader!=null)
                    reader.Close();

            }
        }

    }
}
