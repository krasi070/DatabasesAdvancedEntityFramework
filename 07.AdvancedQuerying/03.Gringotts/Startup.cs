namespace _03.Gringotts
{
    using Data;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            PrintDepositGroupsWithTotalDepositSumBelow150000();
        }

        // Problem 19 - Deposits Sum for Ollivander Family
        private static void PrintDepositGroupsWithTotalDepositSum()
        {
            using (var context = new GringottsContext())
            {
                var depositGroups = context.WizzardDeposits
                    .Where(w => w.MagicWandCreator == "Ollivander family")
                    .GroupBy(w => w.DepositGroup)
                    .Select(g => new
                    {
                        DepositGroup = g.Key,
                        TotalDepositSum = g.Sum(w => w.DepositAmount)
                    })
                    .ToList();

                foreach (var d in depositGroups)
                {
                    Console.WriteLine($"{d.DepositGroup} - {d.TotalDepositSum:F2}");
                }
            }
        }

        // Problem 20 - Deposits Filter
        private static void PrintDepositGroupsWithTotalDepositSumBelow150000()
        {
            using (var context = new GringottsContext())
            {
                var depositGroups = context.WizzardDeposits
                    .Where(w => w.MagicWandCreator == "Ollivander family")
                    .GroupBy(w => w.DepositGroup)
                    .Select(g => new
                    {
                        DepositGroup = g.Key,
                        TotalDepositSum = g.Sum(w => w.DepositAmount)
                    })
                    .Where(d => d.TotalDepositSum < 150000)
                    .ToList()
                    .OrderByDescending(d => d.TotalDepositSum);

                foreach (var d in depositGroups)
                {
                    Console.WriteLine($"{d.DepositGroup} - {d.TotalDepositSum:F2}");
                }
            }
        }
    }
}