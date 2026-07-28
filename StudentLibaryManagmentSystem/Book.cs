using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibaryManagmentSystem
{
    public class Book  // Changed from 'internal' to 'public'
    {
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public BookCategory Category { get; private set; }
        public decimal DailyFee { get; private set; }
        public bool IsAvailable { get; set; }

        public Book(int bookId, string title, string author, BookCategory category,
                    decimal dailyFee, bool isAvailable = true)
        {
            if (bookId <= 0)
                throw new ArgumentException("Book ID must be greater than zero.", nameof(bookId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.", nameof(author));

            if (dailyFee < 0)
                throw new ArgumentException("Daily fee cannot be negative.", nameof(dailyFee));

            BookId = bookId;
            Title = title.Trim();
            Author = author.Trim();
            Category = category;
            DailyFee = dailyFee;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            string availability = IsAvailable ? "Available" : "Borrowed";
            return $"[{BookId}] {Title} by {Author} | {Category} | R{DailyFee:F2}/day | {availability}";
        }

        public static decimal operator +(Book book1, Book book2)
        {
            if (book1 == null) throw new ArgumentNullException(nameof(book1));
            if (book2 == null) throw new ArgumentNullException(nameof(book2));

            return book1.DailyFee + book2.DailyFee;
        }

        public static bool operator ==(Book book1, Book book2)
        {
            if (book1 is null && book2 is null) return true;
            if (book1 is null || book2 is null) return false;

            return book1.BookId == book2.BookId;
        }

        public static bool operator !=(Book book1, Book book2)
        {
            return !(book1 == book2);
        }

        public static bool operator >(Book book1, Book book2)
        {
            if (book1 == null) throw new ArgumentNullException(nameof(book1));
            if (book2 == null) throw new ArgumentNullException(nameof(book2));

            return book1.DailyFee > book2.DailyFee;
        }

        public static bool operator <(Book book1, Book book2)
        {
            if (book1 == null) throw new ArgumentNullException(nameof(book1));
            if (book2 == null) throw new ArgumentNullException(nameof(book2));

            return book1.DailyFee < book2.DailyFee;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Book other)
                return BookId == other.BookId;

            return false;
        }

        public override int GetHashCode()
        {
            return BookId.GetHashCode();
        }
    }
}
