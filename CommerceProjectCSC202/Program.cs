namespace CommerceProjectCSC202
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Please select the method you'd like to run, or press EEE to exit.:");
                Console.WriteLine("1. Get a specific aircraft's info via its callsign.");
                
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
