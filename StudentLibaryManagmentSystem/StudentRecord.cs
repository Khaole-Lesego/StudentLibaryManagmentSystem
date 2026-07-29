using System;

namespace StudentLibaryManagmentSystem
{
    /// <summary>
    /// Represents a student registered in the library system.
    /// This is a record (value‑based reference type) which provides value equality
    /// and immutability by default. 
    /// </summary>
    public record StudentRecord
    {
        /// <summary>Unique identifier for the student (e.g., "ST1001").</summary>
        public string StudentNumber { get; init; }

        /// <summary>The student's full name.</summary>
        public string FullName { get; init; }

        /// <summary>The course the student is enrolled in.</summary>
        public string Course { get; init; }

        /// <summary>
        /// Constructs a new StudentRecord after validating that all fields are non‑empty.
        /// </summary>
        /// <param name="studentNumber">Must not be null or whitespace.</param>
        /// <param name="fullName">Must not be null or whitespace.</param>
        /// <param name="course">Must not be null or whitespace.</param>
        /// <exception cref="ArgumentException">Thrown if any field is invalid.</exception>
        public StudentRecord(string studentNumber, string fullName, string course)
        {
            // Validation ensures the student data is never incomplete.
            if (string.IsNullOrWhiteSpace(studentNumber))
                throw new ArgumentException("Student number cannot be empty.", nameof(studentNumber));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(course))
                throw new ArgumentException("Course cannot be empty.", nameof(course));

            // Once validated, assign the properties (immutable).
            StudentNumber = studentNumber;
            FullName = fullName;
            Course = course;
        }
    }
}