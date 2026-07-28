using System;
using System.Collections.Generic;

namespace StudentLibaryManagmentSystem
{
    class Program
    {
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

        static void RegisterStudent(Library library)
        {
            Console.WriteLine("\n--- Register Student ---");

            Console.Write("Student Number: ");
            string? number = Console.ReadLine();
            Console.Write("Full Name: ");
            string? name = Console.ReadLine();
            Console.Write("Course: ");
            string? course = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(course))
            {
                Console.WriteLine("Error: All fields are required.");
                return;
            }

            library.Student = new StudentRecord(number, name, course);
            Console.WriteLine($"Student {name} registered successfully.");
        }

        static void AddBook(Library library)
        {
            Console.WriteLine("\n--- Add Book ---");

            try
            {
                Console.Write("Book ID: ");
                string? idInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out int id))
                {
                    Console.WriteLine("Error: Invalid ID format. Please enter a number.");
                    return;
                }

                Console.Write("Title: ");
                string? title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Error: Title cannot be empty.");
                    return;
                }

                Console.Write("Author: ");
                string? author = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Error: Author cannot be empty.");
                    return;
                }

                Console.Write("Category (Technology, Science, Literature, History, Other): ");
                string? categoryInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(categoryInput) || !Enum.TryParse(categoryInput, true, out BookCategory category))
                {
                    Console.WriteLine("Invalid category. Please use one of the listed values.");
                    return;
                }

                Console.Write("Daily Fee (e.g., 250.00): ");
                string? feeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(feeInput) || !decimal.TryParse(feeInput, out decimal fee))
                {
                    Console.WriteLine("Error: Invalid fee format. Please enter a valid number.");
                    return;
                }

                Book newBook = new Book(id, title, author, category, fee);
                library.AddBook(newBook);

                Console.WriteLine($"Book '{title}' added successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        static void DisplayBooks(Library library)
        {
            Console.WriteLine("\n--- Display Books ---");
            string display = library.DisplayBooks();
            Console.WriteLine(display);
        }

        static void SearchBook(Library library)
        {
            Console.WriteLine("\n--- Search Book ---");
            Console.Write("Enter title, author, or ID to search: ");

            string? term = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(term))
            {
                Console.WriteLine("Search term cannot be empty.");
                return;
            }

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
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        static void RemoveBook(Library library)
        {
            Console.WriteLine("\n--- Remove Book ---");
            Console.Write("Enter Book ID to remove: ");

            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int id))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                return;
            }

            try
            {
                library.RemoveBook(id);
                Console.WriteLine($"Book with ID {id} removed successfully.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

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
        }

        static void CompareBooks(Library library)
        {
            Console.WriteLine("\n--- Compare Two Books ---");

            Book? book1;
            Book? book2;

            // Get first book
            Console.Write("Enter Book ID of the first book: ");
            string? input1 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input1) || !int.TryParse(input1, out int id1))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                return;
            }

            book1 = library.GetBookById(id1);
            if (book1 is null)
            {
                Console.WriteLine($"No book found with ID {id1}.");
                return;
            }

            // Get second book
            Console.Write("Enter Book ID of the second book: ");
            string? input2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input2) || !int.TryParse(input2, out int id2))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                return;
            }

            book2 = library.GetBookById(id2);
            if (book2 is null)
            {
                Console.WriteLine($"No book found with ID {id2}.");
                return;
            }

            // Display the books
            Console.WriteLine($"\nBook 1: {book1}");
            Console.WriteLine($"Book 2: {book2}");

            // Use null-forgiving operator ! – we know they are not null because of the checks above
            Console.WriteLine($"\n{book1.Title} == {book2.Title} : {book1! == book2!}");
            Console.WriteLine($"{book1.Title} != {book2.Title} : {book1! != book2!}");
            Console.WriteLine($"{book1.Title} > {book2.Title} : {book1! > book2!}");
            Console.WriteLine($"{book1.Title} < {book2.Title} : {book1! < book2!}");

            decimal totalFee = book1! + book2!;
            Console.WriteLine($"Total daily fee for both books: R{totalFee:F2}");
        }
    }
}
