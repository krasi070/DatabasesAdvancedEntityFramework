namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    public class DeleteUser : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // DeleteUser <username>
        public string Execute(params string[] data)
        {
            if (data.Length != 1)
            {
                throw new InvalidOperationException($"Command DeleteUser not valid!");
            }

            string username = data[0];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User with {username} was not found!");
                }

                if (username != Data.CurrentUser.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (user.IsDeleted == null ? false : (bool)user.IsDeleted)
                {
                    throw new InvalidOperationException($"User {user.Username} is already deleted!");
                }

                user.IsDeleted = true;
                context.SaveChanges();

                return $"User {username} was deleted from the database!";
            }
        }
    }
}
