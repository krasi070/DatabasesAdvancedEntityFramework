namespace _03.OldestFamilyMember
{
    using System;
    using _01.DefineClassPerson;
    public class Program
    {
        public static void Main()
        {
            Console.Write("Number of people: ");
            int n = int.Parse(Console.ReadLine());
            Family family = new Family();

            for (int i = 0; i < n; i++)
            {
                string[] args = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Person person = new Person(args[0], int.Parse(args[1]));
                family.AddMember(person);
            }

            Person oldestMember = family.GetOldestMember();
            Console.WriteLine($"Oldest: {oldestMember.Name} {oldestMember.Age}");
        }
    }
}