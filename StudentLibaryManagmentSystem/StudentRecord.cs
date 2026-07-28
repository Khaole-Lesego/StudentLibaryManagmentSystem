using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibaryManagmentSystem
{ 
        public record StudentRecord
        {
            public string StudentNumber { get; init; }
            public string Name { get; init; }
            public string Course { get; init; }

            public StudentRecord(string studentNumber, string fullName, string course)
            {
                if (string.IsNullOrWhiteSpace(studentNumber))
                    throw new ArgumentException("Student number cannot be empty.", nameof(studentNumber));

                if (string.IsNullOrWhiteSpace(fullName))
                    throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

                if (string.IsNullOrWhiteSpace(course))
                    throw new ArgumentException("Course cannot be empty.", nameof(course));
 
            }
        }
    }


