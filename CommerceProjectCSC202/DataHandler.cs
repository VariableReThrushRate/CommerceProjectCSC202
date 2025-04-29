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
        public static List<Product> Load() 
        {
            try
            {
                var json = File.ReadAllText("CommerceData.json");
                List<Product> list = JsonSerializer.Deserialize<List<Product>>(json);
                return list;
            }
            catch (FileNotFoundException) 
            {
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

            string json = JsonSerializer.Serialize(products);
            File.WriteAllText("CommerceData.json", json);
            Console.WriteLine("Data saved!");
        }
    }
}
