namespace _04.Students
{
    using System;

    public class Program
    {
        public static void Main()
        {
            string line = Console.ReadLine();
            while (line.ToLower() != "end")
            {
                Student student = new Student(line);

                line = Console.ReadLine();
            }

            Console.WriteLine($"Instances created: {Student.InstancesCount}");
        }
    }
}