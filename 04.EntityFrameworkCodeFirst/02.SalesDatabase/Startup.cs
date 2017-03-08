namespace _02.SalesDatabase
{
    using Data;

    public class Startup
    {
        // Problem 03 - Sales Database
        // Problem 04 - Products Migration
        // Problem 05 - Sales Migration
        // Problem 06 - Customers Migration
        // Problem 07 - Add Default Age
        public static void Main()
        {
            SalesContext context = new SalesContext();
            context.Database.Initialize(true);
        }
    }
}