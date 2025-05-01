using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    internal class Randomizer
    {
        public static void RandomizerWorker(ref bool run) 
        {
            Random rand = new Random();
            while (run) 
            {
                List<Product> products = DataHandler.Load();
                Thread.Sleep(rand.Next(500, 2501));
                foreach (Product product in products) 
                {
                    if (rand.Next(0, 101) < 25) 
                    {
                        Console.WriteLine(product.ProductName + " was purchased at random!");
                    }
                }
                DataHandler.Save(products);
            }
           
        } 
    }
}
