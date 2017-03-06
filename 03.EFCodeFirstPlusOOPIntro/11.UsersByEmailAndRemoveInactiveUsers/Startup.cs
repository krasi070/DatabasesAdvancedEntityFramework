namespace _11.UsersByEmailAndRemoveInactiveUsers
{
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            UserContext context = new UserContext();

            PrintUsersByEmailProvider(context);
            Console.WriteLine("---------------------------------------------------------");
            RemoveUsersAfterGivenDate(context);
        }

        // Problem 11 - Get Users by Email Provider
        private static void PrintUsersByEmailProvider(UserContext context)
        {
            Console.Write("Email Provider: ");
            string emailProvider = Console.ReadLine();

            context.Users
                .Where(u => u.Email.EndsWith(emailProvider))
                .ToList()
                .ForEach(u =>
                {
                    Console.WriteLine($"{u.Username} {u.Email}");
                });
        }

        // Problem 12 - Remove Inactive Users
        private static void RemoveUsersAfterGivenDate(UserContext context)
        {
            Console.Write("Date: ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            var users = context.Users
                .Where(u => u.LastTimeLoggedIn != null &&
                DateTime.Compare((DateTime)u.LastTimeLoggedIn, date) < 0)
                .ToList();

            foreach (var user in users)
            {
                context.Users.Remove(user);
            }

            context.SaveChanges();

            if (users.Count == 0)
            {
                Console.WriteLine("No users have been deleted.");
            }
            else if (users.Count == 1)
            {
                Console.WriteLine($"{users.Count} user has been deleted.");
            }
            else
            {
                Console.WriteLine($"{users.Count} users have been deleted.");
            }
        }
    }
}