namespace _01.CreateStudentsXMLDocument.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Exams = new HashSet<Exam>();
        }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string University { get; set; }

        public string Specialty { get; set; }

        public string FacultyNumber { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public override string ToString()
        {
            string result = $"Name: {this.Name}\nGender: {this.Gender}\nBirthDate: {this.BirthDate}\nEmail: {this.Email}\nExams:";
            foreach (var exam in this.Exams)
            {
                result += $"\n--{exam.Name} ({exam.Grade})";
            }

            return result;
        }
    }
}