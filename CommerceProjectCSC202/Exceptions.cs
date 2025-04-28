using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceProjectCSC202
{
    public class InvalidSelectionException : Exception
    {
        // Default constructor
        public InvalidSelectionException() : base("This is the base one")
        {
            Console.WriteLine("");
        }

        // Constructor that takes a custom message
        public InvalidSelectionException(string message) : base(message)
        {
        }

        // Constructor that takes a custom message and inner exception
        public InvalidSelectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public class OutOfStockException : Exception
    {
        // Default constructor
        public OutOfStockException() : base("This is the base one")
        {
            Console.WriteLine("Product is out of stock!");
        }

        // Constructor that takes a custom message
        public OutOfStockException(string message) : base(message)
        {
        }

        // Constructor that takes a custom message and inner exception
        public OutOfStockException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
