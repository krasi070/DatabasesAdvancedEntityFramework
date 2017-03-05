namespace _02.CreatePersonConstructors
{
    using System;
    using _01.DefineClassPerson;

    public class Program
    {
        public static void Main()
        {
            string[] args = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Person person;
            if (args.Length == 2)
            {
                person = new Person(args[0], int.Parse(args[1]));
            }
            else if (args.Length == 1)
            {
                int age = 0;
                if (int.TryParse(args[0], out age))
                {
                    person = new Person(age);
                }
                else
                {
                    person = new Person(args[0]);
                }
                
            }
            else
            {
                person = new Person();
            }

            Console.WriteLine($"Name: {person.Name}\nAge: {person.Age}");
        }
    }
}