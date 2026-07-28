using StudentLibaryManagmentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace StudentLibaryManagmentSystem
{
    public class Library
    {
        // Make Student nullable because initially no student is registered
        public StudentRecord? Student { get; set; }

        private readonly List<Book> _books;

        public Library()
        {
            _books = new List<Book>();
            Student = null;
        }

        public void AddBook(Book book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");

            if (_books.Any(b => b.BookId == book.BookId))
                throw new InvalidOperationException($"A book with ID {book.BookId} already exists in the library.");

            _books.Add(book);
        }

        public void RemoveBook(int bookId)
        {
            Book? bookToRemove = _books.FirstOrDefault(b => b.BookId == bookId);

            if (bookToRemove is null)
                throw new InvalidOperationException($"No book found with ID {bookId}.");

            _books.Remove(bookToRemove);
        }

        public List<Book> SearchBook(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty.", nameof(searchTerm));

            if (int.TryParse(searchTerm, out int bookId))
            {
                Book? book = _books.FirstOrDefault(b => b.BookId == bookId);
                return book is not null ? new List<Book> { book } : new List<Book>();
            }
            else
            {
                return _books.Where(b =>
                    b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
        }

        public string DisplayBooks()
        {
            if (_books.Count == 0)
                return "No books in the library.";

            return string.Join(Environment.NewLine, _books.Select(b => b.ToString()));
        }

        public decimal CalculateTotalBorrowingFee()
        {
            return _books.Where(b => !b.IsAvailable).Sum(b => b.DailyFee);
        }


        public Book? GetBookById(int bookId)
        {
            return _books.FirstOrDefault(b => b.BookId == bookId);
        }
    }
}
