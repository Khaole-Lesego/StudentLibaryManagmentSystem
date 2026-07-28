using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibaryManagmentSystem
{
    /// <summary>
    /// Represents the category of a book.
    /// Using an enum ensures that only predefined categories can be assigned,
    /// reducing errors and improving code readability.
    /// </summary>
    public enum BookCategory
    {
        Technology,   // Books related to technology, programming, engineering
        Science,      // Scientific disciplines (physics, chemistry, biology, etc.)
        Literature,   // Fiction, poetry, plays, and literary works
        History,      // Historical texts and accounts
        Other         // Any category not covered by the above
    }
}
