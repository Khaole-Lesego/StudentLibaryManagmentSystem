using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibaryManagmentSystem
{
    /// <summary>
    /// Represents a book in the library.
    /// This class enforces validation on creation and provides operators for:
    /// - Adding daily fees (+)
    /// - Equality based on BookId (==, !=)
    /// - Comparison based on DailyFee (>, <)
    /// It also overrides Equals and GetHashCode for consistency.
    /// </summary>
    public class Book
    {
        // --- Properties ---

        /// <summary>Unique identifier for the book. Immutable after construction.</summary>
        public int BookId { get; private set; }

        /// <summary>Title of the book. Immutable.</summary>
        public string Title { get; private set; }

        /// <summary>Author of the book. Immutable.</summary>
        public string Author { get; private set; }

        /// <summary>Category of the book (from the BookCategory enum). Immutable.</summary>
        public BookCategory Category { get; private set; }

        /// <summary>Daily borrowing fee in South African Rand. Immutable.</summary>
        public decimal DailyFee { get; private set; }

        /// <summary>Indicates whether the book is currently available for borrowing. Mutable.</summary>
        public bool IsAvailable { get; set; }

        // --- Constructor ---

        /// <summary>
        /// Constructs a new Book after performing thorough validation.
        /// </summary>
        /// <param name="bookId">Must be greater than zero.</param>
        /// <param name="title">Must not be null or whitespace.</param>
        /// <param name="author">Must not be null or whitespace.</param>
        /// <param name="category">A valid BookCategory value.</param>
        /// <param name="dailyFee">Must be zero or positive.</param>
        /// <param name="isAvailable">Defaults to true (available).</param>
        /// <exception cref="ArgumentException">Thrown if any validation fails.</exception>
        public Book(int bookId, string title, string author, BookCategory category,
                    decimal dailyFee, bool isAvailable = true)
        {
            // ---- Validation ----
            // Each validation throws a specific ArgumentException with a clear message.
            // This ensures that no Book object can ever exist in an invalid state.

            if (bookId <= 0)
                throw new ArgumentException("Book ID must be greater than zero.", nameof(bookId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.", nameof(author));

            if (dailyFee < 0)
                throw new ArgumentException("Daily fee cannot be negative.", nameof(dailyFee));

            // ---- Assignment ----
            // Only after validation passes do we assign the values.
            // Trim strings to remove leading/trailing spaces.
            BookId = bookId;
            Title = title.Trim();
            Author = author.Trim();
            Category = category;
            DailyFee = dailyFee;
            IsAvailable = isAvailable;
        }

        // --- Overridden Methods ---

        /// <summary>
        /// Returns a formatted string representing the book.
        /// Includes ID, Title, Author, Category, Fee, and Availability.
        /// This is used for displaying books in the console UI.
        /// </summary>
        public override string ToString()
        {
            string availability = IsAvailable ? "Available" : "Borrowed";
            return $"[{BookId}] {Title} by {Author} | {Category} | R{DailyFee:F2}/day | {availability}";
        }

        /// <summary>
        /// Determines whether this book is equal to another object.
        /// Equality is based solely on BookId.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Book other)
                return BookId == other.BookId;
            return false;
        }

        /// <summary>
        /// Returns a hash code based on BookId.
        /// This is required to be consistent with Equals.
        /// </summary>
        public override int GetHashCode()
        {
            return BookId.GetHashCode();
        }

        // --- Operator Overloads ---

        /// <summary>
        /// Adds the DailyFee of two books and returns the total as a decimal.
        /// </summary>
        /// <exception cref="ArgumentNullException">If either operand is null.</exception>
        public static decimal operator +(Book book1, Book book2)
        {
            // Defensive check to avoid NullReferenceException.
            if (book1 == null) throw new ArgumentNullException(nameof(book1));
            if (book2 == null) throw new ArgumentNullException(nameof(book2));

            return book1.DailyFee + book2.DailyFee;
        }

        /// <summary>
        /// Compares two books by BookId for equality.
        /// </summary>
        public static bool operator ==(Book book1, Book book2)
        {
            // Handle null cases: both null -> equal; one null -> not equal.
            if (book1 is null && book2 is null) return true;
            if (book1 is null || book2 is null) return false;

            // Compare by the unique identifier.
            return book1.BookId == book2.BookId;
        }

        /// <summary>
        /// Compares two books by BookId for inequality.
        /// </summary>
        public static bool operator !=(Book book1, Book book2)
        {
            return !(book1 == book2);
        }

        /// <summary>
        /// Compares two books by DailyFee (greater than).
        /// </summary>
        /// <exception cref="ArgumentNullException">If either operand is null.</exception>
        public static bool operator >(Book book1, Book book2)
        {
            if (book1 == null) throw new ArgumentNullException(nameof(book1));
            if (book2 == null) throw new ArgumentNullException(nameof(book2));

            return book1.DailyFee > book2.DailyFee;
        }

        /// <summary>
        /// Compares two books by DailyFee (less than).
        /// </summary>
        /// <exception cref="ArgumentNullException">If either operand is null.</exception>
        public static bool operator <(Book book1, Book book2)
        {
            if (book1 == null) throw new ArgumentNullException(nameof(book1));
            if (book2 == null) throw new ArgumentNullException(nameof(book2));

            return book1.DailyFee < book2.DailyFee;
        }
    }
}
