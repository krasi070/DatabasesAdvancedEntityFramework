namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Models;
    using System.Linq;

    public class RegisterUserCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return false;
            }
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(params string[] data)
        {
            if (Data.IsUserLoggedIn)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (data.Length != 4)
            {
                throw new InvalidOperationException($"Command RegisterUser not valid!");
            }

            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Users.FirstOrDefault(u => u.Username == username) != null)
                {
                    throw new InvalidOperationException($"Username {username} is already taken!");
                }

                if (password != repeatPassword)
                {
                    throw new ArgumentException("Passwords do not match!");
                }

                User user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now,
                    LastTimeLoggedIn = DateTime.Now
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return "User " + username + " was registered successfully!";
        }
    }
}
