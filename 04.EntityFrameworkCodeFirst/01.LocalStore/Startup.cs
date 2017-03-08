namespace _01.LocalStore
{
    public class Startup
    {
        // Problem 01 - Local Store
        // Problem 02 - Local Store Improvement
        public static void Main()
        {
            LocalStoreContext context = new LocalStoreContext();
            context.Database.Initialize(true);
        }
    }
}