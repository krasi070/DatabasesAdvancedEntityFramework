namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    /// Login <username> <password>
    public class LoginCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return false;
            }
        }

        public string Execute(params string[] data)
        {
            if (Data.IsUserLoggedIn)
            {
                throw new ArgumentException("You should logout first!");
            }

            string username = data[0];
            string password = data[1];
            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password!");
                }

                Data.CurrentUser = user;
            }

            return $"User {username} successfully logged in!";
        }
    }
}