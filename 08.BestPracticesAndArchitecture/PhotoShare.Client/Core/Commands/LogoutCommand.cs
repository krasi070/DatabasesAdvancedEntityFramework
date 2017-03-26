namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class LogoutCommand : IExecutable
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
            if (!Data.IsUserLoggedIn)
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            string username = Data.CurrentUser.Username;
            Data.CurrentUser = null;

            return $"User {username} successfully logged out!";
        }
    }
}