namespace CommerceProjectCSC202
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            //Variable Initialization area.
            List<Product> products = new List<Product>();
            List<Product> cart = new List<Product>();


            while (true)
            {
                // This interface system is based on my previous CSC 200 project. It's too good not to reuse for a console based commerce system!
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Search for a product by name.");
                Console.WriteLine("2. Get more details on a product by ID.");
                Console.WriteLine("3. Add a Product to your cart.");
                Console.WriteLine("4. Remove a Product from Cart.");
                Console.WriteLine("5. Check out your cart.");
                Console.WriteLine("6. Register a product.");
                Console.WriteLine("7. Delist a product.");

                Console.Write("Insert selection here: ");

                string brug = Console.ReadLine();
                try
                {
                    //exit line
                    if (brug == "EEE")
                    {
                        break;
                    }
                    int sel = Convert.ToInt32(brug);
                    if (sel >= 1 && sel <= 12) // Update that value whenever you add a method:
                    {
                        switch (sel)
                        {
                            case 1:
                                throw new NotImplementedException();
                                break;
                            case 2:
                                throw new NotImplementedException();
                                break;
                            case 3:
                                throw new NotImplementedException();
                                break;
                            case 4:
                                throw new NotImplementedException();
                                break;
                            case 5:
                                throw new NotImplementedException();
                                break;
                            case 6:
                                throw new NotImplementedException();
                                break;
                            case 7:
                                throw new NotImplementedException();
                                break;
                            default:
                                Console.WriteLine("How did you get here???");
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidSelectionException();
                    }
                }
                catch (NotImplementedException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("That function is not implimented:" + exception.ToString());
                    //Console.Write("That did not work. Please try again : ");
                    //brug = Console.ReadLine();
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nThat did not work. Please try again.\n");
                    Console.Write(exception.ToString());
                    //brug = Console.ReadLine();
                }
            }
        }
    }
}
