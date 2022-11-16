using System;

namespace StudentRegistryApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Birthplace { get; set; }
    }

    // Improvement: A new model a without an id field can be implemented to prevent the overhead while giving the input at create and update functions.
}
