namespace _08.CreateUser
{
    public class Startup
    {
        public static void Main()
        {
            UserContext context = new UserContext();
            context.Database.Initialize(true);
        }
    }
}