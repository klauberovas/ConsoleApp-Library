namespace Library;

class Program
{
    static void Main(string[] args)
    {
        var library = new List<Book>() {
            new("ADD;1984;George Orwell;1949-06-08;328"),
            new("ADD;Animal Farm;George Orwell;1945-08-17;112"),
            new("ADD;Brave New World;Aldous Huxley;1932-01-01;311"),
            new("ADD;Fahrenheit 451;Ray Bradbury;1953-10-19;194"),
            new("ADD;The Great Gatsby;F. Scott Fitzgerald;1925-04-10;180"),
            new("ADD;To Kill a Mockingbird;Harper Lee;1960-07-11;281")
        };

        while (true)
        {
            Console.WriteLine("============== LIBRARY MENU ===============");
            Console.WriteLine("Enter a command:");
            Console.WriteLine("  ADD;[title];[author];[published date in format YYYY-MM-DD];[pages]");
            Console.WriteLine("  LIST        - show all books sorted by date");
            Console.WriteLine("  STATS       - show average pages, books per author, unique title words");
            Console.WriteLine("  FIND;[word] - search books by title");
            Console.WriteLine("  END         - exit program");
            Console.WriteLine("===========================================");

            string input = Console.ReadLine().Trim();

            if (input.ToUpper().StartsWith("ADD;"))
            {
                try
                {
                    Book book = new Book(input);
                    library.Add(book);
                    Console.WriteLine("Book added.");
                }
                catch (ArgumentException e)
                {

                    Console.WriteLine($"Error: {e.Message}"); ;
                }
            }
            else if (input.ToUpper().StartsWith("FIND;"))
            {
                string[] parts = input.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (parts.Length < 2)
                {
                    Console.WriteLine("Missing search keyword.");
                    continue;
                }

                string keyword = parts[1];
                ShowBooksByTitle(library, keyword);
            }
            else
            {
                switch (input.ToUpper())
                {
                    case "LIST":
                        ShowBooks(library);
                        break;

                    case "STATS":
                        ShowBookStats(library);
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

    private static void ShowBooksByTitle(List<Book> library, string keyword)
    {
        var results = library.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        if (!results.Any())
        {
            Console.WriteLine($"Nothing was found for '{keyword}'.");
        }
        else
        {
            Console.WriteLine($"Search result for: '{keyword}':");
            foreach (var book in results)
            {
                Console.WriteLine($"Book: {book.Title} from {book.Author}.");
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

    private static void ShowBookStats(List<Book> library)
    {
        var averagePages = (int)Math.Round(library.Select(b => b.Pages).Average());

        Console.WriteLine("Statistics:");

        Console.WriteLine($"Average number of pages: {averagePages}");

        Console.WriteLine("Books per author:");
        foreach (var group in library.GroupBy(b => b.Author))
        {
            Console.WriteLine($"- {group.Key}: {group.Count()}");
        }

        var uniqueTitleWords = library.SelectMany(b => b.Title
        .Replace(",", "")
        .Replace(".", "")
        .Replace(":", "")
        .Split(" ", StringSplitOptions.RemoveEmptyEntries))
        .Select(w => w.ToLower())
        .Distinct()
        .ToList();

        Console.WriteLine($"Number of unique words in book titles: {uniqueTitleWords.Count()}");
    }
}
