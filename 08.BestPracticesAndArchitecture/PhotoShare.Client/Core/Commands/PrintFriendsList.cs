namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    public class PrintFriendsListCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return false;
            }
        }

        // PrintFriendsList <username>
        public string Execute(params string[] data)
        {
            if (data.Length != 1)
            {
                throw new InvalidOperationException($"Command ListFriends not valid!");
            }

            string username = data[0];
            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.Friends.Count > 0)
                {
                    return string.Format("Friends:\n-{0}", string.Join("\n-", user.Friends.Select(u => u.Username)));
                }
            }

            return "No friends for this user. :(";
        }
    }
}