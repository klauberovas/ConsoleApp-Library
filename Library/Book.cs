using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
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
    }
}