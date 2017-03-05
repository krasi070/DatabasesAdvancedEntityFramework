namespace _06.MathUtilities
{
    using System;

    public class Program
    {
        public static void Main()
        {
            string line = Console.ReadLine();
            while (line.ToLower() != "end")
            {
                string[] args = line
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                double result = 0;
                switch (args[0].ToLower())
                {
                    case "sum":
                        result = MathUtils.Sum(double.Parse(args[1]), double.Parse(args[2]));
                        break;
                    case "subtract":
                        result = MathUtils.Subtract(double.Parse(args[1]), double.Parse(args[2]));
                        break;
                    case "multiply":
                        result = MathUtils.Multiply(double.Parse(args[1]), double.Parse(args[2]));
                        break;
                    case "divide":
                        result = MathUtils.Divide(double.Parse(args[1]), double.Parse(args[2]));
                        break;
                    case "percentage":
                        result = MathUtils.Percentage(double.Parse(args[1]), double.Parse(args[2]));
                        break;
                }

                Console.WriteLine($"{result:F2}");

                line = Console.ReadLine();
            }
        }
    }
}