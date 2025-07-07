using System.Globalization;

namespace Library
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public DateTime PublishedDate { get; }
        private int pages;
        public int Pages
        {
            get { return pages; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The number of pages must be a positive number.");
                }
                pages = value;
            }
        }
        public Book(string title, string author, DateTime publishedDate, int pages)
        {
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            Pages = pages;
        }
        public static Book FromInput(string input)
        {
            string[] parts = input.Split(";", StringSplitOptions.TrimEntries);

            if (parts.Length != 5)
            {
                throw new ArgumentException("Invalid format. Expected: ADD;[title];[author];[YYYY-MM-DD];[pages]");
            }

            string title = parts[1];
            string author = parts[2];

            if (!DateTime.TryParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                throw new ArgumentException("Invalid date format. Please use YYYY-MM-DD.");
            }

            if (!int.TryParse(parts[4], out int parsedPages))
            {
                throw new ArgumentException("Invalid number of pages.");
            }

            return new Book(title, author, parsedDate, parsedPages);
        }
    }
}