namespace _09.HospitalDatabase
{
    public class Startup
    {
        public static void Main()
        {
            HospitalContext context = new HospitalContext();
            context.Database.Initialize(true);
        }
    }
}