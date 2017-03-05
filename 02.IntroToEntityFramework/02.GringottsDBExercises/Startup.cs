namespace _02.GringottsDBExercises
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            GringottsContext context = new GringottsContext();

            PrintFirstLetterOfWizardsWhoHaveDepositType(context, "Troll Chest");
        }

        // Problem 14 - First Letter
        private static void PrintFirstLetterOfWizardsWhoHaveDepositType(GringottsContext context, string depositType)
        {
            var letters = context.WizzardDeposits
                .Where(w => w.DepositGroup == depositType)
                .Select(w => w.FirstName.Substring(0, 1))
                .Distinct();

            foreach (var letter in letters)
            {
                Console.WriteLine(letter);
            }
        }
    }
}