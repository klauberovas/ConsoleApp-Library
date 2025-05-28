namespace Library;

class Program
{
    static void Main(string[] args)
    {
        var bookList = new List<Book>() {
            new("1984", "George Orwell", new DateTime(1949, 6, 8), 328),
            new("Animal Farm", "George Orwell", new DateTime(1945, 8, 17), 112),
            new("Brave New World", "Aldous Huxley", new DateTime(1932, 1, 1), 311),
            new("Fahrenheit 451", "Ray Bradbury", new DateTime(1953, 10, 19), 194),
            new("The Great Gatsby", "F. Scott Fitzgerald", new DateTime(1925, 4, 10), 180),
            new("To Kill a Mockingbird", "Harper Lee", new DateTime(1960, 7, 11), 281)
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
                    Book book = Book.FromInput(input);

                    if (bookList.Any(b => b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase)
                        && b.Author.Equals(book.Author, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("This book is already in the library.");
                    }
                    else
                    {
                        bookList.Add(book);
                        Console.WriteLine("Book added.");
                    }

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
                SearchBooksByTitle(bookList, keyword);
            }

            else
            {

                switch (input.ToUpper())
                {
                    case "LIST":
                        DisplayBooksSortedByDate(bookList);
                        break;

                    case "STATS":
                        DisplayLibraryStatistics(bookList);
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

    private static void SearchBooksByTitle(List<Book> library, string keyword)
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

    private static void DisplayBooksSortedByDate(List<Book> library)
    {
        foreach (var b in library.OrderBy(b => b.PublishedDate))
        {
            Console.WriteLine($"Book: {b.Title}, author: {b.Author}, published: {b.PublishedDate:yyyy.MM.dd}, pages: {b.Pages}");
        }
    }

    private static void DisplayLibraryStatistics(List<Book> library)
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
