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
                Thread.Sleep(rand.Next(500, 2501));
                
            }
        } 
    }
}
