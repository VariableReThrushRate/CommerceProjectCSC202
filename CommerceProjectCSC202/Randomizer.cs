using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    internal class Randomizer
    {
        //This method is designed to run in its own thread.
        public static void RandomizerWorker(ref bool run, ref List<Product> products) 
        {
            //Initialize and run until the bool changes
            Random rand = new Random();
            while (!run) 
            {
                //First, wait a random amount of milliseconds.
                Thread.Sleep(rand.Next(2000, 25001));
                //Then, go over each one.
                foreach (Product product in products) 
                {
                    //12% (Or so, I think) chance to buy something
                    if (rand.Next(0, 101) < 12) 
                    {
                        Console.WriteLine(product.ProductName + " was purchased at random!");
                        //Also buys a random amount of a given item.
                        product.ReduceStock(rand.Next(0, 8));
                    }
                }
            }
           
        } 
    }
}
