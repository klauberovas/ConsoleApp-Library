namespace Library;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("============== LIBRARY MENU ===============");
            Console.WriteLine("Enter a command:");
            Console.WriteLine("  ADD;[title],[author];[published date in format YYYY-MM-DD];[pages]");
            Console.WriteLine("  LIST        - show all books sorted by date");
            Console.WriteLine("  STATS       - show average pages, books per author, unique title words");
            Console.WriteLine("  FIND;[word] - search books by title");
            Console.WriteLine("  END         - exit program");
            Console.WriteLine("===========================================");

            string input = Console.ReadLine().Trim();

            if (input.ToUpper().StartsWith("ADD;"))
            {

            }
            else if (input.ToUpper().StartsWith("FIND;"))
            {

            }
            else
            {
                switch (input.ToUpper())
                {
                    case "LIST":
                        break;

                    case "STATS":
                        break;

                    case "END":
                        return;

                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }
    }
}
