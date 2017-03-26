namespace PhotoShare.Client.Core
{
    using Models;

    public static class Data
    {
        public static User CurrentUser { get; set; }

        public static bool IsUserLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}