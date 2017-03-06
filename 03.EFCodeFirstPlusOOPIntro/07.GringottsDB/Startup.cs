namespace _07.GringottsDB
{
    using System;
    using _07.GringottsDB.Models;

    public class Startup
    {
        public static void Main()
        {
            var context = new WizardContext();
            //context.Database.Initialize(true);

            WizardDeposit wd = new WizardDeposit()
            {
                Id = 1,
                FirstName = "Albus",
                LastName = "Dumbledore",
                Age = 150,
                MagicWandCreator = "Antioch Peverell",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24m,
                DepositCharge = 0.2m,
                IsDepositExpired = false
            };

            context.WizardDeposits.Add(wd);
            context.SaveChanges();
        }
    }
}