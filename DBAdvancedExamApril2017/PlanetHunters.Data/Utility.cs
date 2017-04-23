namespace PlanetHunters.Data
{
    public static class Utility
    {
        public static void InitDB()
        {
            var context = new PlanetHuntersContext();
            context.Database.Initialize(true);
        }
    }
}