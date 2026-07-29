using System;
using System.Collections.Generic;

namespace StudentLibaryManagmentSystem
{
    /// <summary>
    /// The main entry point of the application. Handles the console menu loop
    /// and delegates operations to the Library class.
    /// All input methods now loop until valid data is entered, improving user experience.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main loop: displays the menu, reads the user's choice, and routes
        /// to the corresponding helper method. Continues until the user selects Exit.
        /// </summary>
        static void Main()
        {
            Library library = new Library();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== UNIVERSITY LIBRARY SYSTEM ===");
                Console.WriteLine("1. Register Student");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Display Books");
                Console.WriteLine("4. Search Book");
                Console.WriteLine("5. Remove Book");
                Console.WriteLine("6. Calculate Total Borrowing Fee");
                Console.WriteLine("7. Compare Two Books");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1": RegisterStudent(library); break;
                    case "2": AddBook(library); break;
                    case "3": DisplayBooks(library); break;
                    case "4": SearchBook(library); break;
                    case "5": RemoveBook(library); break;
                    case "6": CalculateTotalFee(library); break;
                    case "7": CompareBooks(library); break;
                    case "0":
                        running = false;
                        Console.WriteLine("Exiting system. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // ================================================================
        //                    HELPER INPUT METHODS
        // ================================================================

        /// <summary>
        /// Prompts the user for a non‑empty string. Loops until a valid value is entered.
        /// </summary>
        /// <param name="prompt">The message displayed to the user.</param>
        /// <param name="allowCancel">If true, entering "cancel" returns null.</param>
        /// <returns>The non‑empty string, or null if cancelled.</returns>
        static string? ReadNonEmptyString(string prompt, bool allowCancel = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (allowCancel && string.Equals(input, "cancel", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("Error: Input cannot be empty. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the user for a positive integer. Loops until a valid value is entered.
        /// </summary>
        /// <param name="prompt">The message displayed to the user.</param>
        /// <param name="allowCancel">If true, entering "cancel" returns null.</param>
        /// <returns>The positive integer, or null if cancelled.</returns>
        static int? ReadPositiveInt(string prompt, bool allowCancel = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (allowCancel && string.Equals(input, "cancel", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (int.TryParse(input, out int value) && value > 0)
                    return value;

                Console.WriteLine("Error: Please enter a positive integer (e.g., 1, 42).");
            }
        }

        /// <summary>
        /// Prompts the user for a non‑negative decimal (fee). Loops until a valid value is entered.
        /// </summary>
        /// <param name="prompt">The message displayed to the user.</param>
        /// <param name="allowCancel">If true, entering "cancel" returns null.</param>
        /// <returns>The non‑negative decimal, or null if cancelled.</returns>
        static decimal? ReadNonNegativeDecimal(string prompt, bool allowCancel = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (allowCancel && string.Equals(input, "cancel", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (decimal.TryParse(input, out decimal value) && value >= 0)
                    return value;

                Console.WriteLine("Error: Please enter a valid non‑negative number (e.g., 250.00).");
            }
        }

        /// <summary>
        /// Prompts the user to select a valid BookCategory from the enum.
        /// Loops until a valid category is entered.
        /// </summary>
        /// <param name="allowCancel">If true, entering "cancel" returns null.</param>
        /// <returns>The selected BookCategory, or null if cancelled.</returns>
        static BookCategory? ReadBookCategory(bool allowCancel = false)
        {
            // Display available categories.
            Console.WriteLine("Available categories: Technology, Science, Literature, History, Other");

            while (true)
            {
                Console.Write("Enter category: ");
                string? input = Console.ReadLine();

                if (allowCancel && string.Equals(input, "cancel", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (Enum.TryParse(input, true, out BookCategory category))
                    return category;

                Console.WriteLine("Error: Invalid category. Please choose from the list above.");
            }
        }

        // ================================================================
        //                    MENU OPTION HANDLERS
        // ================================================================

        /// <summary>
        /// Registers a student. Loops until valid input is provided or the user cancels.
        /// </summary>
        static void RegisterStudent(Library library)
        {
            Console.WriteLine("\n--- Register Student (type 'cancel' to abort) ---");

            string? number = ReadNonEmptyString("Student Number: ", allowCancel: true);
            if (number is null) { Console.WriteLine("Registration cancelled."); return; }

            string? name = ReadNonEmptyString("Full Name: ", allowCancel: true);
            if (name is null) { Console.WriteLine("Registration cancelled."); return; }

            string? course = ReadNonEmptyString("Course: ", allowCancel: true);
            if (course is null) { Console.WriteLine("Registration cancelled."); return; }

            // Create and assign the StudentRecord.
            library.Student = new StudentRecord(number, name, course);
            Console.WriteLine($"Student '{name}' registered successfully.");
        }

        /// <summary>
        /// Adds a book. Loops until valid input is provided or the user cancels.
        /// Handles exceptions from the Library class gracefully.
        /// </summary>
        static void AddBook(Library library)
        {
            Console.WriteLine("\n--- Add Book (type 'cancel' at any prompt to abort) ---");

            while (true)
            {
                // ---- Read each field using helper methods ----
                int? id = ReadPositiveInt("Book ID: ", allowCancel: true);
                if (id is null) { Console.WriteLine("Add Book cancelled."); return; }

                string? title = ReadNonEmptyString("Title: ", allowCancel: true);
                if (title is null) { Console.WriteLine("Add Book cancelled."); return; }

                string? author = ReadNonEmptyString("Author: ", allowCancel: true);
                if (author is null) { Console.WriteLine("Add Book cancelled."); return; }

                // Show categories before asking.
                BookCategory? category = ReadBookCategory(allowCancel: true);
                if (category is null) { Console.WriteLine("Add Book cancelled."); return; }

                decimal? fee = ReadNonNegativeDecimal("Daily Fee (e.g., 250.00): ", allowCancel: true);
                if (fee is null) { Console.WriteLine("Add Book cancelled."); return; }

                // ---- Attempt to create and add the book ----
                try
                {
                    // The Book constructor validates its own arguments.
                    Book newBook = new Book(id.Value, title, author, category.Value, fee.Value);
                    library.AddBook(newBook); // May throw if duplicate ID.

                    Console.WriteLine($"Book '{title}' added successfully.");
                    return; // Exit the method on success.
                }
                catch (ArgumentException ex)
                {
                    // If the Book constructor fails (e.g., empty title, negative fee), show error and loop.
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please correct the input and try again.");
                }
                catch (InvalidOperationException ex)
                {
                    // Duplicate ID – the user can try a different ID.
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please enter a different Book ID.");
                }
                // Continue the loop to ask for all inputs again.
            }
        }

        /// <summary>
        /// Displays all books in the library.
        /// </summary>
        static void DisplayBooks(Library library)
        {
            Console.WriteLine("\n--- Display Books ---");
            string display = library.DisplayBooks();
            Console.WriteLine(display);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(); // Pause so the user can read the output.
        }

        /// <summary>
        /// Searches for books. Loops until valid input is provided or the user cancels.
        /// </summary>
        static void SearchBook(Library library)
        {
            Console.WriteLine("\n--- Search Book (type 'cancel' to abort) ---");

            while (true)
            {
                string? term = ReadNonEmptyString("Enter title, author, or ID to search: ", allowCancel: true);
                if (term is null) { Console.WriteLine("Search cancelled."); return; }

                try
                {
                    List<Book> results = library.SearchBook(term);

                    if (results.Count == 0)
                    {
                        Console.WriteLine("No matching books found.");
                    }
                    else
                    {
                        Console.WriteLine($"Found {results.Count} matching book(s):");
                        foreach (var book in results)
                        {
                            Console.WriteLine($"  {book}");
                        }
                    }

                    // After displaying results, ask if the user wants to search again.
                    Console.Write("\nSearch again? (y/n): ");
                    string? again = Console.ReadLine();
                    if (string.Equals(again, "y", StringComparison.OrdinalIgnoreCase))
                        continue; // Start a new search.
                    else
                        return; // Exit the method.
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    // Loop to try again.
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return; // Exit on unexpected error.
                }
            }
        }

        /// <summary>
        /// Removes a book by ID. Loops until valid input is provided or the user cancels.
        /// </summary>
        static void RemoveBook(Library library)
        {
            Console.WriteLine("\n--- Remove Book (type 'cancel' to abort) ---");

            while (true)
            {
                int? id = ReadPositiveInt("Enter Book ID to remove: ", allowCancel: true);
                if (id is null) { Console.WriteLine("Remove Book cancelled."); return; }

                try
                {
                    library.RemoveBook(id.Value);
                    Console.WriteLine($"Book with ID {id} removed successfully.");
                    return; // Exit on success.
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try a different ID.");
                    // Loop to try again.
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    return;
                }
            }
        }

        /// <summary>
        /// Calculates and displays the total borrowing fee.
        /// </summary>
        static void CalculateTotalFee(Library library)
        {
            Console.WriteLine("\n--- Calculate Total Borrowing Fee ---");

            decimal total = library.CalculateTotalBorrowingFee();

            if (total == 0)
            {
                Console.WriteLine("No books are currently borrowed. Total fee: R0.00");
            }
            else
            {
                Console.WriteLine($"Total Borrowing Fee: R{total:F2}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(); // Pause.
        }

        /// <summary>
        /// Compares two books. Loops until valid IDs are provided or the user cancels.
        /// Demonstrates the overloaded operators.
        /// </summary>
        static void CompareBooks(Library library)
        {
            Console.WriteLine("\n--- Compare Two Books (type 'cancel' to abort) ---");

            while (true)
            {
                // ---- Get first book ----
                int? id1 = ReadPositiveInt("Enter Book ID of the first book: ", allowCancel: true);
                if (id1 is null) { Console.WriteLine("Compare cancelled."); return; }

                Book? book1 = library.GetBookById(id1.Value);
                if (book1 is null)
                {
                    Console.WriteLine($"No book found with ID {id1.Value}. Please try again.");
                    continue;
                }

                // ---- Get second book ----
                int? id2 = ReadPositiveInt("Enter Book ID of the second book: ", allowCancel: true);
                if (id2 is null) { Console.WriteLine("Compare cancelled."); return; }

                Book? book2 = library.GetBookById(id2.Value);
                if (book2 is null)
                {
                    Console.WriteLine($"No book found with ID {id2.Value}. Please try again.");
                    continue;
                }

                // ---- Both books found – display comparison ----
                Console.WriteLine($"\nBook 1: {book1}");
                Console.WriteLine($"Book 2: {book2}");

                Console.WriteLine($"\n{book1.Title} == {book2.Title} : {book1 == book2}");
                Console.WriteLine($"{book1.Title} != {book2.Title} : {book1 != book2}");
                Console.WriteLine($"{book1.Title} > {book2.Title} : {book1 > book2}");
                Console.WriteLine($"{book1.Title} < {book2.Title} : {book1 < book2}");

                decimal totalFee = book1 + book2;
                Console.WriteLine($"Total daily fee for both books: R{totalFee:F2}");

                // Ask if the user wants to compare again.
                Console.Write("\nCompare another pair? (y/n): ");
                string? again = Console.ReadLine();
                if (string.Equals(again, "y", StringComparison.OrdinalIgnoreCase))
                    continue;
                else
                    return;
            }
        }
    }
}
