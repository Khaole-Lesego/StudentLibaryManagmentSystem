using StudentLibaryManagmentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace StudentLibaryManagmentSystem
{
    /// <summary>
    /// Represents the library system. Holds the list of books and the registered student.
    /// This class acts as the business logic layer, coordinating all operations.
    /// </summary>
    public class Library
    {
        // --- Properties ---

        /// <summary>
        /// The student currently registered in the system.
        /// Nullable because no student may be registered yet.
        /// </summary>
        public StudentRecord? Student { get; set; }

        // --- Fields ---

        /// <summary>
        /// Internal list of books. Private to enforce that all modifications go through
        /// the public methods, which include validation.
        /// </summary>
        private readonly List<Book> _books;

        // --- Constructor ---

        /// <summary>
        /// Initializes a new library with an empty book list and no registered student.
        /// </summary>
        public Library()
        {
            _books = new List<Book>();
            Student = null;
        }

        // --- Public Methods ---

        /// <summary>
        /// Adds a book to the library.
        /// </summary>
        /// <param name="book">The book to add. Must not be null and must have a unique BookId.</param>
        /// <exception cref="ArgumentNullException">If book is null.</exception>
        /// <exception cref="InvalidOperationException">If another book with the same ID already exists.</exception>
        public void AddBook(Book book)
        {
            // Guard against null input.
            if (book is null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");

            // Check for duplicate ID to enforce uniqueness.
            if (_books.Any(b => b.BookId == book.BookId))
                throw new InvalidOperationException($"A book with ID {book.BookId} already exists in the library.");

            // All checks passed – add the book.
            _books.Add(book);
        }

        /// <summary>
        /// Removes a book from the library by its ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to remove.</param>
        /// <exception cref="InvalidOperationException">If no book with the given ID is found.</exception>
        public void RemoveBook(int bookId)
        {
            // Find the book by ID (return null if not found).
            Book? bookToRemove = _books.FirstOrDefault(b => b.BookId == bookId);

            // If not found, throw an exception – the operation is invalid.
            if (bookToRemove is null)
                throw new InvalidOperationException($"No book found with ID {bookId}.");

            // Remove the book from the list.
            _books.Remove(bookToRemove);
        }

        /// <summary>
        /// Searches for books that match the given search term.
        /// The search handles both numeric ID and text (title/author).
        /// </summary>
        /// <param name="searchTerm">Can be a BookId (number) or a substring of title/author.</param>
        /// <returns>A list of matching books (empty if none found).</returns>
        /// <exception cref="ArgumentException">If searchTerm is null or whitespace.</exception>
        public List<Book> SearchBook(string searchTerm)
        {
            // Validate input.
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty.", nameof(searchTerm));

            // Try to parse as an integer – treat as BookId search.
            if (int.TryParse(searchTerm, out int bookId))
            {
                // Find a book with that exact ID.
                Book? book = _books.FirstOrDefault(b => b.BookId == bookId);
                // Return a list containing the book if found, otherwise an empty list.
                return book is not null ? new List<Book> { book } : new List<Book>();
            }
            else
            {
                // Search by title or author (case‑insensitive, partial match).
                return _books.Where(b =>
                    b.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
        }

        /// <summary>
        /// Returns a formatted string listing all books in the library.
        /// Uses each book's ToString() method.
        /// </summary>
        /// <returns>A multi‑line string or a message if the library is empty.</returns>
        public string DisplayBooks()
        {
            if (_books.Count == 0)
                return "No books in the library.";

            // Join all book strings with a newline separator.
            return string.Join(Environment.NewLine, _books.Select(b => b.ToString()));
        }

        /// <summary>
        /// Calculates the total daily borrowing fee for all books that are currently
        /// marked as borrowed (IsAvailable == false).
        /// </summary>
        /// <returns>The sum of DailyFee for borrowed books, or 0 if none.</returns>
        public decimal CalculateTotalBorrowingFee()
        {
            return _books.Where(b => !b.IsAvailable).Sum(b => b.DailyFee);
        }

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <param name="bookId">The ID of the book to find.</param>
        /// <returns>The Book object if found; otherwise, null.</returns>
        public Book? GetBookById(int bookId)
        {
            return _books.FirstOrDefault(b => b.BookId == bookId);
        }
    }
}