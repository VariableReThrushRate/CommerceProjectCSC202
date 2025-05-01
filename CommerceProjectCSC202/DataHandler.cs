using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Numerics;

namespace CommerceProjectCSC202
{
    internal class DataHandler
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
        IncludeFields = true,
        PropertyNameCaseInsensitive = true
        };
    public static List<Product> Load() 
        {
            LoadStatics();
            try
            {
                var json = File.ReadAllText("CommerceData.json");
                List<Product> list = JsonSerializer.Deserialize<List<Product>>(json, options);
                return list;
            }
            catch (FileNotFoundException) 
            {
                Console.WriteLine("No data found. Initializing!");
                return new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Returning issue:");
                Console.WriteLine(ex);
                return null;
            }
        }
        public static void Save(List<Product> products) 
        {
            //Could these be combined into one big try catch? Yes. Will I do so? No, because I don't want to.
            SaveStatics();
            try
            {
                File.Delete("CommerceData.json.bak");
            }
            catch 
            {
                Console.WriteLine("Save backup didn't exist. Proceeding.");
            }
            try
            {
                File.Move("CommerceData.json", "CommerceData.json.bak", true);
            }
            catch
            {
                Console.WriteLine("Main save didn't exist. Creating.");
            }

            try
            {
                string json = JsonSerializer.Serialize(products, options);
                File.WriteAllText("CommerceData.json", json);
                //Console.WriteLine("Data saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong!");
                Console.WriteLine(ex);
            }
        }
        public static void LoadStatics() 
        {
            try
            {
                var json = File.ReadAllText("Indexer.json");
                Dictionary<string, int> dict = JsonSerializer.Deserialize<Dictionary<string, int>>(json, options);
                Customer.Setid(dict["CustomerID"]);
                Manager.Setid(dict["ManagerID"]);
                Product.Setid(dict["ProductID"]);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No indexer. No action needed!");
            }
        }
        public static void SaveStatics()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic["ManagerID"] = Manager.GetID();
            dic["CustomerID"] = Customer.GetID();
            dic["ProductID"] = Product.GetID();
            try
            {
                File.Delete("Indexer.json");
                string json = JsonSerializer.Serialize(dic);
                File.WriteAllText("Indexer.json", json);
                //Console.WriteLine("Data saved!");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No indexer. Saving instead!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Dumping!");
                Console.WriteLine(ex.ToString());
            }
        }
        public static List<Customer> LoadCustomers()
        {
            try
            {
                var json = File.ReadAllText("Customerdata.json");
                List<Customer> list = JsonSerializer.Deserialize<List<Customer>>(json, options);
                return list;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No data found. Initializing!");
                return new List<Customer>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Returning issue:");
                Console.WriteLine(ex);
                return null;
            }
        }
        public static List<Manager> LoadManagers()
        {
            try
            {
                var json = File.ReadAllText("Managerdata.json");
                List<Manager> list = JsonSerializer.Deserialize<List<Manager>>(json, options);
                return list;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No data found. Initializing!");
                return new List<Manager>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Returning issue:");
                Console.WriteLine(ex);
                return null;
            }
        }
        public static void SaveCustomer(List<Customer> customers)
        {
            //Could these be combined into one big try catch? Yes. Will I do so? No, because I don't want to.
            try
            {
                File.Delete("Customerdata.json.bak");
            }
            catch
            {
                Console.WriteLine("Save backup didn't exist. Proceeding.");
            }
            try
            {
                File.Move("Customerdata.json", "Customerdata.json.bak", true);
            }
            catch
            {
                Console.WriteLine("Customer save didn't exist. Creating.");
            }

            try
            {
                string json = JsonSerializer.Serialize(customers, options);
                File.WriteAllText("Customerdata.json", json);
                //Console.WriteLine("Data saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong!");
                Console.WriteLine(ex);
            }
        }
        public static void SaveManager(List<Manager> managers)
        {
            //Could these be combined into one big try catch? Yes. Will I do so? No, because I don't want to.
            try
            {
                File.Delete("Managerdata.json.bak");
            }
            catch
            {
                Console.WriteLine("Save backup didn't exist. Proceeding.");
            }
            try
            {
                File.Move("Managerdata.json", "Managerdata.json.bak", true);
            }
            catch
            {
                Console.WriteLine("Manager save didn't exist. Creating.");
            }

            try
            {
                string json = JsonSerializer.Serialize(managers, options);
                File.WriteAllText("Managerdata.json", json);
                //Console.WriteLine("Data saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong!");
                Console.WriteLine(ex);
            }
        }
    }
}
