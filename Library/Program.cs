namespace Library;

class Program
{
    static void Main(string[] args)
    {
        var library = new List<Book>() {
            new("1984", "George Orwell", new DateTime(1949,06,08), 328),
            new("Animal Farm", "George Orwell", new DateTime(1945, 08, 17), 112),
            new("Brave New World", "Aldous Huxley",new DateTime(1932, 01,01),311),
            new("Fahrenheit 451", "Ray Bradbury", new DateTime(1953, 10, 19), 194),
            new("The Great Gatsby", "F. Scott Fitzgerald", new DateTime(1925, 4, 10), 180),
            new("To Kill a Mockingbird", "Harper Lee", new DateTime(1960, 7, 11), 281)
        };

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
                        ShowBooks(library);
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

    private static void ShowBooks(List<Book> library)
    {
        foreach (var b in library.OrderBy(b => b.PublishedDate))
        {
            Console.WriteLine($"Book: {b.Title}, author: {b.Author}, published: {b.PublishedDate:yyyy.MM.dd}, pages: {b.Pages}");
        }
    }
}
