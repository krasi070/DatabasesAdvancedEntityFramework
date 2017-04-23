namespace PlanetHunters.Export
{
    using Data.DbManipulation;

    public class Startup
    {
        public static void Main()
        {
            PlanetStore.GetPlanetsByTelescope("Trappist-1");
        }
    }
}