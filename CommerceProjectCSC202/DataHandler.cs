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
            try
            {
                Product.Setid(int.Parse(File.ReadAllText("Indexer.txt")));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No indexer. Initializing!");
                Product.Setid(0);
            }
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
            try
            {
                File.Delete("Indexer.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No indexer. Saving instead!");
            }
            try
            {
                File.WriteAllText("Indexer.txt", Product.GetID().ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Dumping!");
                Console.WriteLine(ex.ToString());
            }
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

            string json = JsonSerializer.Serialize(products, options);
            File.WriteAllText("CommerceData.json", json);
            Console.WriteLine("Data saved!");
        }
    }
}
