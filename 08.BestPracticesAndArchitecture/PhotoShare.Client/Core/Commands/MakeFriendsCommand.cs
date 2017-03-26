namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    public class MakeFriendsCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // MakeFriends <username1> <username2>
        public string Execute(params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command MakeFriends not valid!");
            }

            var username1 = data[0];
            var username2 = data[1];

            if (username1 != Data.CurrentUser.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            using (var context = new PhotoShareContext())
            {
                var user1 = context.Users
                    .FirstOrDefault(u => u.Username == username1);
                var user2 = context.Users
                    .FirstOrDefault(u => u.Username == username2);
                if (user1 == null)
                {
                    throw new ArgumentException($"User {username1} not found!");
                }

                if (user2 == null)
                {
                    throw new ArgumentException($"User {username2} not found!");
                }

                if (user1.Friends.FirstOrDefault(u => u.Username == username2) != null)
                {
                    throw new ArgumentException($"{username2} is already a friend to {username1}");
                }

                user1.Friends.Add(user2);
                context.SaveChanges();
            }

            return $"Friend {username2} added to {username1}";
        }
    }
}