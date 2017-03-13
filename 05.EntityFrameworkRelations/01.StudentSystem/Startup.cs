namespace _01.StudentSystem
{
    using System;
    using System.Linq;
    using Data;

    public class Startup
    {
        // Problem 01 - Code First Student System
        // Problem 02 - Seed Some Data in the Database
        // Problem 03 - Working with the Database
        // Problem 04 - Resource Licenses
        public static void Main()
        {
            StudentSystemContext context = new StudentSystemContext();
            context.Database.Initialize(true);
        }

        private static void ListAllStudentsAndTheirHomeworks(StudentSystemContext context)
        {
            foreach (var student in context.Students)
            {
                Console.WriteLine(student.Name + ":");
                foreach (var hw in student.Homeworks)
                {
                    Console.WriteLine($"--{hw.Content} | {hw.ContentType}");
                }
            }
        }

        private static void ListAllCoursesAndResources(StudentSystemContext context)
        {
            var courses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate);
            foreach (var course in courses)
            {
                Console.WriteLine($"Name: {course.Name}");
                Console.WriteLine($"Description: {course.Description}");
                Console.Write("Resources: ");
                foreach (var resource in course.Resources)
                {
                    Console.Write($"{resource.Name} (Type: {resource.Type}; URL: {resource.Url});");
                }

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        private static void ListAllCoursesWithMoreThan5Resources(StudentSystemContext context)
        {
            var courses = context.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate);

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name}: {course.Resources.Count}");
            }
        }

        private static void ListActiveCourses(StudentSystemContext context)
        {
            DateTime date = DateTime.Now;

            var courses = context.Courses
                .Where(c => c.StartDate.CompareTo(date) <= 0 && c.EndDate.CompareTo(date) >= 0)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    NumberOfStudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.NumberOfStudentsEnrolled)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Name: {course.Name}");
                Console.WriteLine($"Start date: {course.StartDate}");
                Console.WriteLine($"End date: {course.EndDate}");
                Console.WriteLine($"Duration in days: {(course.EndDate - course.StartDate).TotalDays}");
                Console.WriteLine($"Number of students enrolled: {course.NumberOfStudentsEnrolled}");
                Console.WriteLine("--------------------------------------------------------------------");
            }
        }

        private static void ListStudentCourses(StudentSystemContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Select(c => c.Price).Sum()
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name);

            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Courses: {student.NumberOfCourses}");
                Console.WriteLine($"Total price: {student.TotalPrice}");
                Console.WriteLine($"Average price: {student.TotalPrice / student.NumberOfCourses}");
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }
    }
}