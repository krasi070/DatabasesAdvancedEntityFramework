namespace _01.StudentSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using Models;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudentSystemContext context)
        {
            if (context.Students.Count() == 0 &&
                context.Courses.Count() == 0 &&
                context.Resources.Count() == 0 &&
                context.Homeworks.Count() == 0)
            {
                context.Courses.RemoveRange(context.Courses);
                context.Resources.RemoveRange(context.Resources);
                context.Homeworks.RemoveRange(context.Homeworks);
                // Students
                var ivan = new Student()
                {
                    Name = "Ivan",
                    RegistrationDate = DateTime.Now
                };

                var krasi = new Student()
                {
                    Name = "Krasi",
                    RegistrationDate = DateTime.Now
                };

                var martin = new Student()
                {
                    Name = "Martin",
                    RegistrationDate = DateTime.Now
                };

                // Courses
                var cSharp = new Course()
                {
                    Name = "C# Basics",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Price = 100m,

                };

                var java = new Course()
                {
                    Name = "Java Basics",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Price = 150m
                };

                // Resources
                var introVideo = new Resource()
                {
                    Name = "Intro to C#",
                    Type = "video",
                    Url = "www.softuni.com/video?id=1731781636"
                };

                var homework = new Resource()
                {
                    Name = "First C# HW",
                    Type = "document",
                    Url = "www.softuni.com/hw?id=1731781636"
                };

                var presentation = new Resource()
                {
                    Name = "Intro to C# (presentation)",
                    Type = "presentation",
                    Url = "www.softuni.com/pt?id=1731781636"
                };

                // Homeworks
                var hw1 = new Homework()
                {
                    Content = "dewihdewuihdweihdwuihduiw",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now
                };

                var hw2 = new Homework()
                {
                    Content = "dewihdewuihdwedewdwihdwuihduiw",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now
                };

                var hw3 = new Homework()
                {
                    Content = "dewihdddwqwdqdewuihdweihdwuihduiw",
                    ContentType = "zip",
                    SubmissionDate = DateTime.Now
                };

                // Adding students to courses
                cSharp.Students.Add(krasi);
                cSharp.Students.Add(martin);
                java.Students.Add(ivan);
                java.Students.Add(martin);

                // Adding courses to students
                krasi.Courses.Add(cSharp);
                ivan.Courses.Add(java);
                martin.Courses.Add(cSharp);
                martin.Courses.Add(java);

                // Adding homeworks
                krasi.Homeworks.Add(hw1);
                ivan.Homeworks.Add(hw2);
                martin.Homeworks.Add(hw3);
                cSharp.Homeworks.Add(hw1);
                java.Homeworks.Add(hw2);
                java.Homeworks.Add(hw3);

                // Adding resources
                cSharp.Resources.Add(introVideo);
                cSharp.Resources.Add(homework);
                cSharp.Resources.Add(presentation);

                // Adding entities to db
                context.Students.AddOrUpdate(
                    s => s.Name,
                    ivan,
                    krasi,
                    martin
                );

                context.Courses.AddOrUpdate(
                    c => c.Name,
                    cSharp,
                    java
                );

                context.Resources.AddOrUpdate(
                    r => r.Url,
                    introVideo,
                    homework,
                    presentation
                );

                context.Homeworks.AddOrUpdate(
                    h => h.Content,
                    hw1,
                    hw2,
                    hw3
                );

                this.SaveChanges(context);
            }

            if (context.Licenses.Count() == 0)
            {
                // Licenses
                var l1 = new License()
                {
                    Name = "1",
                    ResourceId = 1
                };

                var l2 = new License()
                {
                    Name = "2",
                    ResourceId = 1
                };

                context.Licenses.AddOrUpdate(
                    l => l.Name,
                    l1,
                    l2
                );

                this.SaveChanges(context);
            }
        }

        // method gotten from http://stackoverflow.com/questions/10219864/ef-code-first-how-do-i-see-entityvalidationerrors-property-from-the-nuget-pac
        private void SaveChanges(StudentSystemContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}