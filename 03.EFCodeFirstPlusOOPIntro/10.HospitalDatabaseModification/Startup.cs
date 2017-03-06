namespace _10.HospitalDatabaseModification
{
    public class Startup
    {
        static void Main()
        {
            HospitalContext context = new HospitalContext();
            context.Database.Initialize(true);
        }
    }
}