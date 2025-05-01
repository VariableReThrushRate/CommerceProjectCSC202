using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    internal class Randomizer
    {
        public static void RandomizerWorker(ref bool run, ref List<Product> products) 
        {
            Random rand = new Random();
            while (!run) 
            {
                Thread.Sleep(rand.Next(2000, 25001));
                foreach (Product product in products) 
                {
                    if (rand.Next(0, 101) < 25) 
                    {
                        Console.WriteLine(product.ProductName + " was purchased at random!");
                        product.ReduceStock(rand.Next(0, 8));
                    }
                }
            }
           
        } 
    }
}
