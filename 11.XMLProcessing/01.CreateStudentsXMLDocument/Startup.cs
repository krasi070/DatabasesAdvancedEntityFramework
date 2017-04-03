namespace _01.CreateStudentsXMLDocument
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            XDocument xDoc = XDocument.Load("../../Import/student.xml");
            var student = ParseStudent(xDoc.Root);
            Console.WriteLine(student.ToString());
        }

        private static Student ParseStudent(XElement root)
        {
            var exams = new HashSet<Exam>();
            var examsXml = root.Element("exams").Elements();
            foreach (var exam in examsXml)
            {
                exams.Add(new Exam()
                {
                    Name = exam.Element("name").Value,
                    DateTaken = DateTime.Parse(exam.Element("dateTaken").Value),
                    Grade = double.Parse(exam.Element("grade").Value)
                });
            }

            return new Student()
            {
                Name = root.Element("name").Value,
                Gender = root.Element("gender").Value,
                BirthDate = DateTime.Parse(root.Element("birthDate").Value),
                PhoneNumber = root.Element("phoneNumber").Value,
                Email = root.Element("email").Value,
                University = root.Element("university").Value,
                Specialty = root.Element("specialty").Value,
                FacultyNumber = root.Element("facultyNumber").Value,
                Exams = exams
            };
        }
    }
}